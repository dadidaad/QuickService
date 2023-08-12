using AutoMapper;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.WorkflowAssignment;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class WorkflowAssignmentService : IWorkflowAssignmentService
    {
        private readonly IWorkflowAssignmentRepository _repository;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IWorkflowTaskRepository _workflowTaskRepository;
        private readonly IRequestTicketRepository _requestTicketRepository;
        private readonly IWorkflowTransitionRepository _workflowTransitionRepository;
        private readonly IAttachmentService _attachmentService;
        private readonly ISlaRepository _slaRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public WorkflowAssignmentService(IWorkflowAssignmentRepository repository
            , IWorkflowRepository workflowRepository
            , IRequestTicketRepository requestTicketRepository
            , IWorkflowTaskRepository WorkflowTaskRepository
            , IAttachmentService attachmentService
            , IMapper mapper
            , IWorkflowTransitionRepository workflowTransitionRepository
            , ISlaRepository slaRepository
            , IUserRepository userRepository)
        {
            _repository = repository;
            _workflowRepository = workflowRepository;
            _requestTicketRepository = requestTicketRepository;
            _workflowTaskRepository = WorkflowTaskRepository;
            _attachmentService = attachmentService;
            _mapper = mapper;
            _workflowTransitionRepository = workflowTransitionRepository;
            _slaRepository = slaRepository;
            _userRepository = userRepository;
        }

        private static readonly Dictionary<StatusEnum, StatusWorkflowTaskEnum> StatusMapping = new Dictionary<StatusEnum, StatusWorkflowTaskEnum>
        {
            { StatusEnum.Open, StatusWorkflowTaskEnum.Open },
            { StatusEnum.InProgress, StatusWorkflowTaskEnum.InProgress},
            { StatusEnum.Pending, StatusWorkflowTaskEnum.Pending },
            { StatusEnum.Resolved, StatusWorkflowTaskEnum.Resolved}
        };

        public async Task AssignWorkflow(List<string> workflowTasks, RequestTicket requestTicket)
        {
            var workflowAssignment = new WorkflowAssignment
            {
                ReferenceId = requestTicket.RequestTicketId
            };
            var workflow = await _workflowRepository.GetWorkflowById(requestTicket.WorkflowId);
            var sla = await _slaRepository.GetSlaForRequestTicket(requestTicket);
            workflowAssignment.DueDate = DateTime.Now
                + TimeSpan.FromTicks(sla.Slametrics.FirstOrDefault(sm => sm.Priority == requestTicket.Priority).ResolutionTime);
            if (workflowTasks.Count == 0)
            {
                HandleForNoneConfigureTransition(workflowAssignment, workflow);
            }
            else
            {
                if (workflowTasks.Count > 1)
                {
                    using (var workflowTaskEnumerator = workflowTasks.GetEnumerator())
                    {
                        var listWorkflowAssignments = new List<WorkflowAssignment>();
                        string currentId = await GetNextId();
                        if (workflowTaskEnumerator.MoveNext())
                        {
                            var workflowAssignmentClone = workflowAssignment.DeepCopy();
                            workflowAssignmentClone.WorkflowAssignmentId = currentId;
                            workflowAssignmentClone.CurrentTaskId = workflowTaskEnumerator.Current;
                        }
                        while (workflowTaskEnumerator.MoveNext())
                        {
                            var workflowAssignmentClone = workflowAssignment.DeepCopy();
                            var nextId = GenerateNextIdForWorkflowAssignment(currentId);
                            var workflowTask = workflowTaskEnumerator.Current;
                            workflowAssignmentClone.WorkflowAssignmentId = nextId;
                            workflowAssignmentClone.CurrentTaskId = workflowTask;
                            currentId = nextId;
                            listWorkflowAssignments.Add(workflowAssignmentClone);

                        }
                        await _repository.AddRangeWorkflowAssignment(listWorkflowAssignments);
                    }
                }
                else
                {
                    workflowAssignment.WorkflowAssignmentId = await GetNextId();
                    workflowAssignment.CurrentTaskId = workflowTasks.FirstOrDefault();
                    await _repository.AddWorkflowAssignment(workflowAssignment);
                }
            }
        }

        private async void HandleForNoneConfigureTransition(WorkflowAssignment workflowAssignment, Workflow workflow)
        {
            List<WorkflowTask> workflowTasks = workflow.WorkflowTasks.ToList();
            if (workflowTasks.Count > 1)
            {
                using (var workflowTaskEnumerator = workflowTasks.GetEnumerator())
                {
                    var listWorkflowAssignments = new List<WorkflowAssignment>();
                    string currentId = await GetNextId();
                    if (workflowTaskEnumerator.MoveNext())
                    {
                        var workflowAssignmentClone = workflowAssignment.DeepCopy();
                        workflowAssignmentClone.WorkflowAssignmentId = currentId;
                        workflowAssignmentClone.CurrentTaskId = workflowTaskEnumerator.Current.WorkflowTaskId;
                    }
                    while (workflowTaskEnumerator.MoveNext())
                    {
                        var workflowAssignmentClone = workflowAssignment.DeepCopy();
                        var nextId = GenerateNextIdForWorkflowAssignment(currentId);
                        var workflowTask = workflowTaskEnumerator.Current;
                        workflowAssignmentClone.WorkflowAssignmentId = nextId;
                        workflowAssignmentClone.CurrentTaskId = workflowTask.WorkflowTaskId;
                        currentId = nextId;
                        listWorkflowAssignments.Add(workflowAssignmentClone);

                    }
                    await _repository.AddRangeWorkflowAssignment(listWorkflowAssignments);
                }
            }
            else
            {
                workflowAssignment.WorkflowAssignmentId = await GetNextId();
                workflowAssignment.CurrentTaskId = workflowTasks.FirstOrDefault().WorkflowTaskId;
                await _repository.AddWorkflowAssignment(workflowAssignment);
            }
        }

        private async Task<WorkflowDAG> WorkflowTransitionToDAG(Workflow workflow)
        {
            List<WorkflowTransition> workflowTransitionsForWorkflow = await _workflowTransitionRepository.GetWorkflowTransitionsByWorkflow(workflow.WorkflowId);
            return new WorkflowDAG(workflowTransitionsForWorkflow);
        }

        public async Task CompleteWorkflowTask(CheckWorkflowAssignmentDTO checkWorkflowAssignmentDTO)
        {
            var workflowAssignment = await _repository
                .GetWorkflowAssignmentByWorkflowAssignmentId(checkWorkflowAssignmentDTO.WorkflowAssignmentId);
            if (workflowAssignment == null)
            {
                throw new AppException($"Workflow assignment with id {checkWorkflowAssignmentDTO.WorkflowAssignmentId} not found");
            }
            var requestTicket = await _requestTicketRepository.GetRequestTicketById(workflowAssignment.ReferenceId);

            var currentWorkflowTask = await _workflowTaskRepository.GetWorkflowTaskById(workflowAssignment.CurrentTaskId);

            if (currentWorkflowTask.AssignerId == null)
            {
                throw new AppException($"Workflow task not yet assign to any agent!");
            }
            var user = await _userRepository.GetUserDetails(checkWorkflowAssignmentDTO.ReceiverId);
            if(user == null)
            {
                throw new AppException($"User with id {checkWorkflowAssignmentDTO.ReceiverId} not found");
            }
            if (checkWorkflowAssignmentDTO.ReceiverId != currentWorkflowTask.AssignerId)
            {
                throw new AppException($"Workflow task not assign to user have id {checkWorkflowAssignmentDTO.ReceiverId}!");
            }
            if (checkWorkflowAssignmentDTO.IsCompleted)
            {
                List<string> allPreviousTask = await GetAllPreviousTasks(currentWorkflowTask);
                //handle if this step before this status isn't completed
                if (!await CheckAllPreviousStepCompleted(allPreviousTask, requestTicket) && allPreviousTask.Count > 0)
                {
                    throw new AppException("Some tasks before this status isn't completed");
                }
                if (checkWorkflowAssignmentDTO.File != null)
                {
                    workflowAssignment.AttachmentId = (await _attachmentService.CreateAttachment(checkWorkflowAssignmentDTO.File)).AttachmentId;
                }
                var updateWorkflowAssignment = _mapper.Map(checkWorkflowAssignmentDTO, workflowAssignment);
                updateWorkflowAssignment.CompletedTime = DateTime.Now;
                if (workflowAssignment.IsReject)
                {
                    updateWorkflowAssignment.IsReject = false;
                    updateWorkflowAssignment.RejectReason = null;
                }
                await _repository.UpdateWorkflowAssignment(updateWorkflowAssignment);
                requestTicket.Status = MappingWorkflowTaskStatusToRequestTicketStatus(currentWorkflowTask.Status.ToEnum(StatusWorkflowTaskEnum.Open)).ToString();
                requestTicket.AssignedTo = checkWorkflowAssignmentDTO.ReceiverId;
                await _requestTicketRepository.UpdateRequestTicket(requestTicket);
                var nextWorkflowTasks = await GetNextWorkflowTasks(currentWorkflowTask);
                var workflowAssignments = await _repository.GetWorkflowAssignmentsByRequestTicket(requestTicket.RequestTicketId);
                for (int i = 0; i < nextWorkflowTasks.Count; i++)
                {
                    if (workflowAssignments.Any(wa => wa.CurrentTaskId == nextWorkflowTasks[i]))
                    {
                        nextWorkflowTasks.RemoveAt(i);
                    }
                }
                if (nextWorkflowTasks.Any())
                {
                    await AssignWorkflow(nextWorkflowTasks, requestTicket);
                }
            }
        }


        public async Task<List<string>> GetSourcesTasks(string workflowId)
        {
            var workflow = await _workflowRepository.GetWorkflowById(workflowId);
            WorkflowDAG workflowDAG = await WorkflowTransitionToDAG(workflow);
            return workflowDAG.GetSources();
        }

        private async Task<List<string>> GetNextWorkflowTasks(WorkflowTask currentWorkflowTask)
        {
            WorkflowDAG workflowDAG = await WorkflowTransitionToDAG(currentWorkflowTask.Workflow);
            return workflowDAG.GetAdjacentNodes(currentWorkflowTask.WorkflowTaskId);
        }

        private async Task<List<string>> GetAllPreviousTasks(WorkflowTask currentWorkflowTask)
        {
            WorkflowDAG workflowDAG = await WorkflowTransitionToDAG(currentWorkflowTask.Workflow);
            return workflowDAG.GetPreviousNodes(currentWorkflowTask.WorkflowTaskId);
        }


        private async Task<bool> CheckAllPreviousStepCompleted(List<string> previousWorkflowTasks, RequestTicket requestTicket)
        {
            var currentWorkflowAssignments = await _repository.GetAllCurrentWorkflowAssignments(previousWorkflowTasks, requestTicket);
            return currentWorkflowAssignments.All(wa => wa.IsCompleted);
        }
        public async Task<bool> CheckRequestTicketExists(string requestTicketId)
        {
            return await _repository.CheckExistingRequestTicket(requestTicketId);
        }

        private bool CheckStatusRequestTicketInStatusMapping(StatusEnum statusEnum)
        {
            return StatusMapping.ContainsKey(statusEnum);
        }

        private StatusEnum MappingWorkflowTaskStatusToRequestTicketStatus(StatusWorkflowTaskEnum statusWorkflowTaskEnum)
        {
            return StatusMapping.Where(s => s.Value == statusWorkflowTaskEnum).FirstOrDefault().Key;
        }

        public async Task RejectWorkflowTask(RejectWorkflowTaskDTO rejectWorkflowTaskDTO)
        {
            var workflowAssignment = await _repository
                .GetWorkflowAssignmentByWorkflowAssignmentId(rejectWorkflowTaskDTO.WorkflowAssignmentId);
            if (workflowAssignment == null)
            {
                throw new AppException($"Workflow assignment with id {rejectWorkflowTaskDTO.WorkflowAssignmentId} not found");
            }
            var requestTicket = await _requestTicketRepository.GetRequestTicketById(workflowAssignment.ReferenceId);
            var currentWorkflowTask = await _workflowTaskRepository.GetWorkflowTaskById(workflowAssignment.CurrentTaskId);
            if (rejectWorkflowTaskDTO.IsReject)
            {
                var updateWorkflowAssignment = _mapper.Map(rejectWorkflowTaskDTO, workflowAssignment);
                updateWorkflowAssignment.IsCompleted = false;
                if (updateWorkflowAssignment.AttachmentId != null)
                {
                    await _attachmentService.DeleteAttachment(updateWorkflowAssignment.AttachmentId);
                }
                updateWorkflowAssignment.CompleteMessage = null;
                updateWorkflowAssignment.CompletedTime = null;
                await _repository.UpdateWorkflowAssignment(updateWorkflowAssignment);
                await HandleRejectAction(currentWorkflowTask, requestTicket);
            }
        }

        private async Task HandleRejectAction(WorkflowTask currentWorkflowTask, RequestTicket requestTicket)
        {
            var allNextWorkflowTasks = await GetNextWorkflowTasks(currentWorkflowTask);
            await AssignWorkflow(allNextWorkflowTasks, requestTicket);
            requestTicket.Status = MappingWorkflowTaskStatusToRequestTicketStatus(currentWorkflowTask.Status.ToEnum(StatusWorkflowTaskEnum.Open)).ToString();
            await _requestTicketRepository.UpdateRequestTicket(requestTicket);

        }


        private async Task DeleteListWorkflowAssignment(List<WorkflowAssignment> workflowAssignments)
        {
            foreach (var workflowAssignment in workflowAssignments)
            {
                if (!string.IsNullOrEmpty(workflowAssignment.AttachmentId))
                {
                    await _attachmentService.DeleteAttachment(workflowAssignment.AttachmentId);
                }
            }
            await _repository.DeleteRangeWorkflowAssignment(workflowAssignments);
        }

        public async Task<List<WorkflowAssignmentDTO>> GetWorkflowAssignmentsForTicket(string requestTicketId)
        {
            var requestTicket = await _requestTicketRepository.GetRequestTicketById(requestTicketId);
            if (requestTicket == null)
            {
                throw new AppException($"Request ticket with id {requestTicketId} not found");
            }
            var workflowAssignments = await _repository.GetWorkflowAssignmentsByRequestTicket(requestTicketId);
            return workflowAssignments.Select(wa => _mapper.Map<WorkflowAssignmentDTO>(wa)).ToList();
        }

        private string GenerateNextIdForWorkflowAssignment(string currentId)
        {
            int id = IDGenerator.ExtractNumberFromId(currentId) + 1;
            string workflowAssignmentId = IDGenerator.GenerateWorkflowAssignmentId(id);
            return workflowAssignmentId;
        }

        private async Task<string> GetNextId()
        {
            WorkflowAssignment lastWorkflowAssignment = await _repository.GetLastWorkflowAssignment();
            int id;
            if (lastWorkflowAssignment == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastWorkflowAssignment.WorkflowAssignmentId) + 1;
            }
            string workflowAssignmentId = IDGenerator.GenerateWorkflowAssignmentId(id);
            return workflowAssignmentId;
        }
    }
}
