using AutoMapper;
using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.DTOs.WorkflowStep;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IWorkflowRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IServiceItemRepository _serviceItemRepository;
        private readonly ISlaRepository _slaRepository;
        private readonly IMapper _mapper;
        private readonly IWorkflowStepService _workflowStepService;
        private readonly IWorkflowAssignmentRepository _workflowAssignmentRepository;
        public WorkflowService(IWorkflowRepository repository,
            IUserRepository userRepository, IMapper mapper,
            IServiceItemRepository serviceItemRepository, ISlaRepository slaRepository,
            IWorkflowStepService workflowStepService,
            IWorkflowAssignmentRepository workflowAssignmentRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
            _serviceItemRepository = serviceItemRepository;
            _slaRepository = slaRepository;
            _workflowStepService = workflowStepService;
            _workflowAssignmentRepository = workflowAssignmentRepository;
        }

        public async Task<List<WorkflowDTO>> GetWorkflows()
        {
            var workflows = await _repository.GetWorkflows();
            workflows.ForEach(async w => await HandleStatusInWorkflow(w));
            return workflows.Select(workflow => _mapper.Map<WorkflowDTO>(workflow)).ToList();
        }

        public async Task<WorkflowDTO> GetWorkflowById(string workflowId)
        {
            var workflow = await _repository.GetWorkflowById(workflowId);
            if(workflow == null)
            {
                throw new AppException($"Workflow with id {workflowId} not found");
            }
            await HandleStatusInWorkflow(workflow);
            return _mapper.Map<WorkflowDTO>(workflow);
        }

        public async Task CreateWorkflow(CreateUpdateWorkflowDTO createUpdateWorkflowDTO)
        {
            var creator = _userRepository.GetUserDetails(createUpdateWorkflowDTO.CreatedBy);
            if(creator == null)
            {
                throw new AppException($"User with id {createUpdateWorkflowDTO.CreatedBy} not found");
            }
            var workflow = _mapper.Map<Workflow>(createUpdateWorkflowDTO);
            workflow.WorkflowId = await GetNextId();
            workflow.Status = StatusWorkflowEnum.Active.ToString();
            workflow.CreatedAt = DateTime.Now;
            var addedWorkflow = await _repository.AddWorkflow(workflow);
            if(addedWorkflow != null)
            {
                CreateUpdateWorkflowStepDTO createUpdateWorkflowStepDTO = new()
                {
                    WorkflowStepName = "Resolved Step",
                    Status = StatusWorkflowStepEnum.Resolved.ToString(),
                    ActionDetail = "Resolved all step in workflow",
                    WorkflowId = workflow.WorkflowId
                };
                await _workflowStepService.CreateWorkflowStep(createUpdateWorkflowStepDTO);
            }
        }

        public async Task UpdateWorkflow(string workflowId, CreateUpdateWorkflowDTO createUpdateWorkflowDTO)
        {
            Workflow workflow = await _repository.GetWorkflowById(workflowId);
            if (workflow == null)
            {
                throw new AppException("Workflow not found");
            }
            if (_userRepository.GetUserDetails(createUpdateWorkflowDTO.CreatedBy) == null)
            {
                throw new AppException("Create by user with id " + createUpdateWorkflowDTO.CreatedBy + " not found");
            }
            workflow = _mapper.Map(createUpdateWorkflowDTO, workflow);
            workflow.LastUpdate = DateTime.Now;
            await _repository.UpdateWorkflow(workflow);
        }

        public async Task DeleteWorkflow(string workflowId)
        {
            var workflow = await _repository.GetWorkflowById(workflowId);
            await _repository.DeleteWorkflow(workflow);
        }
        public async Task<string> GetNextId()
        {
            Workflow lastWorkflow = await _repository.GetLastWorkflow();
            int id = 0;
            if (lastWorkflow == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastWorkflow.WorkflowId) + 1;
            }
            string workflowId = IDGenerator.GenerateWorkflowId(id);
            return workflowId;
        }

        public async Task AssignWorkflow(AssignWorkflowDTO assignWorkflowDTO)
        {
            var workflow = await _repository.GetWorkflowById(assignWorkflowDTO.WorkflowId);
            if (workflow == null)
            {
                throw new AppException("Workflow not found");
            }
            await HandleWorkflowStepInWorkflow(workflow);
            if(!string.IsNullOrEmpty(assignWorkflowDTO.ReferenceId) && !assignWorkflowDTO.ForIncident)
            {
                var serviceItem = await _serviceItemRepository.GetServiceItemById(assignWorkflowDTO.ReferenceId);
                if(serviceItem == null)
                {
                    throw new AppException($"Service item with id {assignWorkflowDTO.ReferenceId} not found");
                }
                bool mustOnlyOnceWorkflowForService = await _repository.CheckTotalOfWorkflowAssignTo(true, assignWorkflowDTO.ReferenceId) != 0;
                if (mustOnlyOnceWorkflowForService)
                {
                    throw new AppException($"Already have workflow for service item with id {assignWorkflowDTO.ReferenceId}");    
                    
                }
            }
            else if(string.IsNullOrEmpty(assignWorkflowDTO.ReferenceId) && assignWorkflowDTO.ForIncident)
            {
                //option: can have multiple workflow for incident, take newest or only one
                //bool mustOnlyOnceWorkflowForIncident = await _repository.CheckTotalOfWorkflowAssignTo(false, null) != 0;
                //if (mustOnlyOnceWorkflowForIncident)
                //{
                //    throw new AppException($"Already have workflow for incident");
                //}
            }
            else
            {
                throw new AppException("Must declare clearly for incident or service item");
            }
            
            var updateWorkflow = _mapper.Map(assignWorkflowDTO, workflow);
            var sla = await _slaRepository.GetSlaForWorflow(updateWorkflow);
            if (sla != null)
            {
                updateWorkflow.Sla = sla;
            }
            await _repository.UpdateWorkflow(updateWorkflow);
        }

        private async Task HandleStatusInWorkflow(Workflow workflow)
        {
            bool checkAnyWorkflowInUse = (await _workflowAssignmentRepository
                .GetWorkflowAssignmentsByWorkflowId(workflow.WorkflowId)).Any(ws => !ws.IsCompleted);
            if (checkAnyWorkflowInUse)
            {
                workflow.Status = StatusWorkflowEnum.InUse.ToString();
            }
            else if (string.IsNullOrEmpty(workflow.ReferenceId) && !workflow.ForIncident)
            {
                workflow.Status = StatusWorkflowEnum.InActive.ToString();
            }
            else if(!string.IsNullOrEmpty(workflow.ReferenceId) || workflow.ForIncident)
            {
                workflow.Status = StatusWorkflowEnum.Active.ToString();
            }
            await _repository.UpdateWorkflow(workflow);
        }
        private async Task HandleWorkflowStepInWorkflow(Workflow workflow)
        {
            var steps = workflow.WorkflowSteps.ToList();
            var statusWorkflowStepEnums = Enum.GetValues(typeof(StatusWorkflowStepEnum))
                .Cast<StatusWorkflowStepEnum>().ToList();
            if(steps.Count == 0)
            {
                throw new AppException("Workflow don't have any steps!!");
            }
            bool containsAllStatus = statusWorkflowStepEnums
                .All(enumValue => steps.Any(step => step.Status == enumValue.ToString()));

            if (!containsAllStatus)
            {
                throw new AppException("Workflow don't have enough steps!!");
            }
        }
    }
}
