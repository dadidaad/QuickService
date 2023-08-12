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
        private readonly IWorkflowTaskRepository _WorkflowTaskRepository;
        private readonly IRequestTicketRepository _requestTicketRepository;
        private readonly IWorkflowTransitionRepository _workflowTransitionRepository;
        private readonly IAttachmentService _attachmentService;
        private readonly ISlaRepository _slaRepository;
        private readonly IMapper _mapper;
        public WorkflowAssignmentService(IWorkflowAssignmentRepository repository
            , IWorkflowRepository workflowRepository
            , IRequestTicketRepository requestTicketRepository
            , IWorkflowTaskRepository WorkflowTaskRepository
            , IAttachmentService attachmentService
            , IMapper mapper
            , IWorkflowTransitionRepository workflowTransitionRepository
            , ISlaRepository slaRepository)
        {
            _repository = repository;
            _workflowRepository = workflowRepository;
            _requestTicketRepository = requestTicketRepository;
            _WorkflowTaskRepository = WorkflowTaskRepository;
            _attachmentService = attachmentService;
            _mapper = mapper;
            _workflowTransitionRepository = workflowTransitionRepository;
            _slaRepository = slaRepository;
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
                    var listWorkflowAssignments = new List<WorkflowAssignment>();
                    foreach (var workflowTaskId in workflowTasks)
                    {
                        var workflowAssignmentClone = workflowAssignment.DeepCopy();
                        workflowAssignmentClone.CurrentTaskId = workflowTaskId;
                        listWorkflowAssignments.Add(workflowAssignmentClone);
                    }
                    await _repository.AddRangeWorkflowAssignment(listWorkflowAssignments);
                }
                else
                {
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
                var listWorkflowAssignments = new List<WorkflowAssignment>();
                foreach (var workflowTask in workflowTasks)
                {
                    var workflowAssignmentClone = workflowAssignment.DeepCopy();
                    workflowAssignmentClone.CurrentTaskId = workflowTask.WorkflowTaskId;
                    listWorkflowAssignments.Add(workflowAssignmentClone);
                }
                await _repository.AddRangeWorkflowAssignment(listWorkflowAssignments);
            }
            else
            {
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
            var requestTicket = await _requestTicketRepository.GetRequestTicketById(checkWorkflowAssignmentDTO.ReferenceId);
            if (requestTicket == null)
            {
                throw new AppException($"Ticket with id {checkWorkflowAssignmentDTO.ReferenceId} not found");
            }
            var currentWorkflowTask = await _WorkflowTaskRepository.GetWorkflowTaskById(checkWorkflowAssignmentDTO.CurrentTaskId);
            if (currentWorkflowTask == null || currentWorkflowTask.WorkflowId != requestTicket.WorkflowId)
            {
                throw new AppException($"Workflow step with id {checkWorkflowAssignmentDTO.CurrentTaskId} not found" +
                    $" or not in workflow");
            }
            if (currentWorkflowTask.AssignerId == null)
            {
                throw new AppException($"Workflow step not yet assign to any agent!");
            }
            if (checkWorkflowAssignmentDTO.ReceiverId != currentWorkflowTask.AssignerId)
            {
                throw new AppException($"Workflow step not assign to user have id {checkWorkflowAssignmentDTO.ReceiverId}!");
            }
            var workflowAssignment = await _repository
                .GetWorkflowAssignmentByCompositeKey(checkWorkflowAssignmentDTO.ReferenceId,
                checkWorkflowAssignmentDTO.CurrentTaskId);
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

        public bool CheckStatusRequestTicketInStatusMapping(StatusEnum statusEnum)
        {
            return StatusMapping.ContainsKey(statusEnum);
        }

        public StatusEnum MappingWorkflowTaskStatusToRequestTicketStatus(StatusWorkflowTaskEnum statusWorkflowTaskEnum)
        {
            return StatusMapping.Where(s => s.Value == statusWorkflowTaskEnum).FirstOrDefault().Key;
        }

        public async Task RejectWorkflowTask(RejectWorkflowTaskDTO rejectWorkflowTaskDTO)
        {
            var requestTicket = await _requestTicketRepository.GetRequestTicketById(rejectWorkflowTaskDTO.ReferenceId);
            if (requestTicket == null)
            {
                throw new AppException($"Ticket with id {rejectWorkflowTaskDTO.ReferenceId} not found");
            }
            var currentWorkflowTask = await _WorkflowTaskRepository.GetWorkflowTaskById(rejectWorkflowTaskDTO.CurrentTaskId);
            if (currentWorkflowTask == null || currentWorkflowTask.WorkflowId != requestTicket.WorkflowId)
            {
                throw new AppException($"Workflow step with id {rejectWorkflowTaskDTO.CurrentTaskId} not found" +
                    $" or not in workflow");
            }
            var workflowAssignment = await _repository
                .GetWorkflowAssignmentByCompositeKey(rejectWorkflowTaskDTO.ReferenceId,
                rejectWorkflowTaskDTO.CurrentTaskId);

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
            var listWorkflowAssignment = await _repository.GetAllCurrentWorkflowAssignments(allNextWorkflowTasks, requestTicket);
            await DeleteListWorkflowAssignment(listWorkflowAssignment);
            requestTicket.Status = MappingWorkflowTaskStatusToRequestTicketStatus(currentWorkflowTask.Status.ToEnum(StatusWorkflowTaskEnum.Open)).ToString();
            await _requestTicketRepository.UpdateRequestTicket(requestTicket);

        }


        public async Task DeleteListWorkflowAssignment(List<WorkflowAssignment> workflowAssignments)
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
    }
}
