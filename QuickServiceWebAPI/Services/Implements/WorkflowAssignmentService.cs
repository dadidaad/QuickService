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
            { StatusEnum.Pending, StatusWorkflowStepEnum.Pending }
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
            var workflowAssignment = await _repository
                .GetWorkflowAssignmentByCompositeKey(checkWorkflowAssignmentDTO.ReferenceId,
                checkWorkflowAssignmentDTO.WorkflowId, checkWorkflowAssignmentDTO.CurrentStepId);
            if (checkWorkflowAssignmentDTO.IsCompleted)
            {
                if (checkWorkflowAssignmentDTO.File != null)
                {
                    workflowAssignment.Attachment = await _attachmentService.CreateAttachment(checkWorkflowAssignmentDTO.File);
                }
                var updateWorkflowAssignment = _mapper.Map(checkWorkflowAssignmentDTO, workflowAssignment);
                updateWorkflowAssignment.CompletedTime = DateTime.Now;
                await _repository.UpdateWorkflowAssignment(updateWorkflowAssignment);
                var workflowAssignments = GetCurrentWorkflowSteps(requestTicket, workflow);
                if(workflowAssignments.Count > 0)
                {
                    await HandleCompleteStepInStatus(workflowAssignments, workflow, requestTicket);
                }
                else
                {
                    throw new AppException("Don't have any workflow steps or workflow finished");
                }
            }
        }

        private async Task HandleCompleteStepInStatus(List<WorkflowStep> currentWorkflowSteps, Workflow workflow, RequestTicket requestTicket)
        {
            var currentWorkflowAssignments = await _repository.GetAllCurrentWorkflowAssignments(currentWorkflowSteps, workflow, requestTicket);
            bool checkAllStepCompleted = currentWorkflowAssignments.All(wa => wa.IsCompleted);
            if (checkAllStepCompleted)
            {
                requestTicket.Status = (requestTicket.Status.ToEnum(StatusEnum.Open) + 1).ToString();
                await _requestTicketRepository.UpdateRequestTicket(requestTicket);
                await AssignWorkflow(requestTicket);
            }
        }

        public async Task<bool> CheckRequestTicketExists(string requestTicketId)
        {
            return await _repository.CheckExistingRequestTicket(requestTicketId);
        }

        public bool CheckStatusRequestTicketInStatusMapping(StatusEnum statusEnum)
        {
            return StatusMapping.ContainsKey(statusEnum);
        }
    }
}
