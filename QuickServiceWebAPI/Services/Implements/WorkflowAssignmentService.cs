using AutoMapper;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IWorkflowStepRepository _workflowStepRepository;
        private readonly IRequestTicketRepository _requestTicketRepository;
        private readonly IAttachmentService _attachmentService;
        private readonly IMapper _mapper;
        public WorkflowAssignmentService(IWorkflowAssignmentRepository repository
            , IWorkflowRepository workflowRepository
            ,IRequestTicketRepository requestTicketRepository
            , IWorkflowStepRepository workflowStepRepository
            , IAttachmentService attachmentService
            , IMapper mapper)
        {
            _repository = repository;
            _workflowRepository = workflowRepository;
            _requestTicketRepository = requestTicketRepository;
            _workflowStepRepository = workflowStepRepository;
            _attachmentService = attachmentService;
            _mapper = mapper;
        }

        private static readonly Dictionary<StatusEnum, StatusWorkflowStepEnum> StatusMapping = new Dictionary<StatusEnum, StatusWorkflowStepEnum>
        {
            { StatusEnum.Open, StatusWorkflowStepEnum.Queued },
            { StatusEnum.InProgress, StatusWorkflowStepEnum.Implementing },
            { StatusEnum.Pending, StatusWorkflowStepEnum.Pending },
            { StatusEnum.Resolved, StatusWorkflowStepEnum.Resolved}
        };

        public async Task AssignWorkflow(RequestTicket requestTicket)
        {
            var workflowAssignment = new WorkflowAssignment
            {
                ReferenceId = requestTicket.RequestTicketId
            };
            Workflow? workflow = null;
            if (requestTicket.ServiceItemId != null && !requestTicket.IsIncident)
            {
                workflow = await _workflowRepository.GetWorkflowAssignTo(true, requestTicket.ServiceItemId);

            }
            else if (requestTicket.ServiceItemId == null && requestTicket.IsIncident)
            {
                workflow = await _workflowRepository.GetWorkflowAssignTo(false, null);
            }
            if (workflow == null)
            {
                throw new AppException($"Don't have valid workflow assign to service item or incident");
            }
            workflowAssignment.WorkflowId = workflow.WorkflowId;
            List<WorkflowStep> workflowStepsForAssign = GetCurrentWorkflowSteps(requestTicket, workflow);
            workflowAssignment.DueDate = DateTime.Now + TimeSpan.FromTicks(workflow.Sla.Slametrics.FirstOrDefault().ResolutionTime);
            if (workflowStepsForAssign.Count > 1)
            {
                var listWorkflowAssignments = new List<WorkflowAssignment>();
                foreach (var workflowStep in workflowStepsForAssign)
                {
                    var workflowAssignmentClone = workflowAssignment.DeepCopy();
                    workflowAssignmentClone.CurrentStepId = workflowStep.WorkflowStepId;
                    listWorkflowAssignments.Add(workflowAssignmentClone);
                }
                await _repository.AddRangeWorkflowAssignment(listWorkflowAssignments);
            }
            else
            {
                workflowAssignment.CurrentStepId = workflowStepsForAssign.FirstOrDefault().WorkflowStepId;
                await _repository.AddWorkflowAssignment(workflowAssignment);
            }
        }

        private List<WorkflowStep> GetCurrentWorkflowSteps(RequestTicket requestTicket, Workflow workflow)
        {
            var workflowStepsForAssign = new List<WorkflowStep>();
            StatusEnum requestTicketStatus = requestTicket.Status.ToEnum(StatusEnum.Open);
            StatusWorkflowStepEnum statusWorkflowStepEnum = StatusMapping
            .FirstOrDefault(x => x.Key == requestTicketStatus).Value;
            var workflowSteps = workflow.WorkflowSteps
                .Where(ws => ws.Status == statusWorkflowStepEnum.ToString()).ToList();
            return workflowStepsForAssign;
        }

        public async Task CompleteWorkflowStep(CheckWorkflowAssignmentDTO checkWorkflowAssignmentDTO)
        {
            var requestTicket = await _requestTicketRepository.GetRequestTicketById(checkWorkflowAssignmentDTO.ReferenceId);
            if (requestTicket == null)
            {
                throw new AppException($"Ticket with id {checkWorkflowAssignmentDTO.ReferenceId} not found");
            }
            var workflow = await _workflowRepository.GetWorkflowById(checkWorkflowAssignmentDTO.WorkflowId);
            if (workflow == null)
            {
                throw new AppException($"Workflow with id {checkWorkflowAssignmentDTO.WorkflowId} not found");
            }
            var currentWorkflowStep = await _workflowStepRepository.GetWorkflowStepById(checkWorkflowAssignmentDTO.CurrentStepId);
            if (currentWorkflowStep == null || currentWorkflowStep.WorkflowId != checkWorkflowAssignmentDTO.WorkflowId)
            {
                throw new AppException($"Workflow step with id {checkWorkflowAssignmentDTO.CurrentStepId} not found" +
                    $" or not in workflow");
            }
            if(currentWorkflowStep.AssignerId != null)
            {
                throw new AppException($"Workflow step not yet assign to any agent!");
            }
            if(checkWorkflowAssignmentDTO.ReceiverId != currentWorkflowStep.AssignerId)
            {
                throw new AppException($"Workflow step not assign to user have id {checkWorkflowAssignmentDTO.ReceiverId}!");
            }
            var workflowAssignment = await _repository
                .GetWorkflowAssignmentByCompositeKey(checkWorkflowAssignmentDTO.ReferenceId,
                checkWorkflowAssignmentDTO.WorkflowId, checkWorkflowAssignmentDTO.CurrentStepId);
            if (checkWorkflowAssignmentDTO.IsCompleted)
            {
                //handle if this step before this status isn't completed
                if(!await CheckAllPreviousStepCompleted(GetAllPreviousSteps(workflow, currentWorkflowStep), workflow, requestTicket))
                {
                    throw new AppException("Some steps before this status isn't completed");
                }
                if (checkWorkflowAssignmentDTO.File != null)
                {
                    workflowAssignment.Attachment = await _attachmentService.CreateAttachment(checkWorkflowAssignmentDTO.File);
                }
                var updateWorkflowAssignment = _mapper.Map(checkWorkflowAssignmentDTO, workflowAssignment);
                updateWorkflowAssignment.CompletedTime = DateTime.Now;
                if (workflowAssignment.IsReject)
                {
                    updateWorkflowAssignment.IsReject = false;
                    updateWorkflowAssignment.RejectReason = null;
                }
                await _repository.UpdateWorkflowAssignment(updateWorkflowAssignment);
                var workflowAssignments = GetCurrentWorkflowSteps(requestTicket, workflow);
                if(workflowAssignments.Count > 0)
                {
                    await HandleCompleteStepInStatus(workflowAssignments, workflow, requestTicket);
                }
                else if(workflowAssignments.Count == 0 && !await _repository.CheckAllWorkflowStepCompleted(workflow.WorkflowSteps.ToList(), requestTicket))
                {
                    throw new AppException("Don't have any workflow steps");
                }
                //else if(await _repository.CheckAllWorkflowStepCompleted(workflow.WorkflowSteps.ToList(), requestTicket))
                //{
                //    var workflowAssignmentForResolved = await _repository
                //.GetWorkflowAssignmentByCompositeKey(checkWorkflowAssignmentDTO.ReferenceId,
                //checkWorkflowAssignmentDTO.WorkflowId, 
                //workflow.WorkflowSteps.FirstOrDefault(ws => ws.Status == StatusWorkflowStepEnum.Resolved.ToString()).WorkflowStepId);
                //}
            }
        }

        private async Task HandleCompleteStepInStatus(List<WorkflowStep> currentWorkflowSteps, Workflow workflow, RequestTicket requestTicket)
        {
            var currentWorkflowAssignments = await _repository.GetAllCurrentWorkflowAssignments(currentWorkflowSteps, workflow, requestTicket);
            bool checkAllStepInStatusCompleted = currentWorkflowAssignments.All(wa => wa.IsCompleted);
            if (checkAllStepInStatusCompleted)
            {
                requestTicket.Status = (requestTicket.Status.ToEnum(StatusEnum.Open) + 1).ToString();
                await _requestTicketRepository.UpdateRequestTicket(requestTicket);
                await AssignWorkflow(requestTicket);
            }
        }

        private async Task<bool> CheckAllPreviousStepCompleted(List<WorkflowStep> currentWorkflowSteps, Workflow workflow, RequestTicket requestTicket)
        {
            var currentWorkflowAssignments = await _repository.GetAllCurrentWorkflowAssignments(currentWorkflowSteps, workflow, requestTicket);
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

        public async Task RejectWorkflowStep(RejectWorkflowStepDTO rejectWorkflowStepDTO)
        {
            var requestTicket = await _requestTicketRepository.GetRequestTicketById(rejectWorkflowStepDTO.ReferenceId);
            if (requestTicket == null)
            {
                throw new AppException($"Ticket with id {rejectWorkflowStepDTO.ReferenceId} not found");
            }
            var workflow = await _workflowRepository.GetWorkflowById(rejectWorkflowStepDTO.WorkflowId);
            if (workflow == null)
            {
                throw new AppException($"Workflow with id {rejectWorkflowStepDTO.WorkflowId} not found");
            }
            var currentWorkflowStep = await _workflowStepRepository.GetWorkflowStepById(rejectWorkflowStepDTO.CurrentStepId);
            if (currentWorkflowStep == null || currentWorkflowStep.WorkflowId != rejectWorkflowStepDTO.WorkflowId)
            {
                throw new AppException($"Workflow step with id {rejectWorkflowStepDTO.CurrentStepId} not found" +
                    $" or not in workflow");
            }
            var workflowAssignment = await _repository
                .GetWorkflowAssignmentByCompositeKey(rejectWorkflowStepDTO.ReferenceId,
                rejectWorkflowStepDTO.WorkflowId, rejectWorkflowStepDTO.CurrentStepId);

            if (rejectWorkflowStepDTO.IsReject)
            {
                var updateWorkflowAssignment = _mapper.Map(rejectWorkflowStepDTO, workflowAssignment);
                updateWorkflowAssignment.IsCompleted = false;
                if(updateWorkflowAssignment.AttachmentId != null)
                {
                    await _attachmentService.DeleteAttachment(updateWorkflowAssignment.AttachmentId);
                }
                updateWorkflowAssignment.CompleteMessage = null;
                updateWorkflowAssignment.CompletedTime = null;
                await _repository.UpdateWorkflowAssignment(updateWorkflowAssignment);
                await HandleRejectAction(workflow, currentWorkflowStep, requestTicket);
            }
        }

        private async Task HandleRejectAction(Workflow workflow, WorkflowStep workflowStep, RequestTicket requestTicket)
        {
            var allNextWorkflowSteps = GetAllNextWorkflowSteps(workflow, workflowStep);
            var listWorkflowAssignment = await _repository.GetAllCurrentWorkflowAssignments(allNextWorkflowSteps, workflow, requestTicket);
            await DeleteListWorkflowAssignment(listWorkflowAssignment);
            requestTicket.Status = StatusMapping
                .FirstOrDefault(x => x.Value == workflowStep.Status.ToEnum(StatusWorkflowStepEnum.Queued))
                .Key.ToString();
            await _requestTicketRepository.UpdateRequestTicket(requestTicket);

        }
        private List<WorkflowStep> GetAllNextWorkflowSteps(Workflow workflow, WorkflowStep workflowStep)
        {
            return workflow.WorkflowSteps
                .Where(ws => ws.Status.ToEnum(StatusWorkflowStepEnum.Queued)
                > workflowStep.Status.ToEnum(StatusWorkflowStepEnum.Queued)).ToList();
        }

        private List<WorkflowStep> GetAllPreviousSteps(Workflow workflow, WorkflowStep workflowStep)
        {
            return workflow.WorkflowSteps
                .Where(ws => ws.Status.ToEnum(StatusWorkflowStepEnum.Queued)
                < workflowStep.Status.ToEnum(StatusWorkflowStepEnum.Queued)).ToList();
        }

        public async Task DeleteListWorkflowAssignment(List<WorkflowAssignment> workflowAssignments)
        {
            foreach(var workflowAssignment in workflowAssignments)
            {
                if(!string.IsNullOrEmpty(workflowAssignment.AttachmentId))
                {
                    await _attachmentService.DeleteAttachment(workflowAssignment.AttachmentId);
                }
            }
            await _repository.DeleteRangeWorkflowAssignment(workflowAssignments);
        }

        public Task<List<WorkflowAssignmentDTO>> GetWorkflowAssignmentsForTicket(string requestTicketId)
        {
            throw new NotImplementedException();
        }
    }
}
