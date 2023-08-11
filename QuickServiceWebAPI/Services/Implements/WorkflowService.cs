using AutoMapper;
using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.DTOs.WorkflowTask;
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
        private readonly IRequestTicketRepository _requestTicketRepository;
        private readonly ISlaRepository _slaRepository;
        private readonly IMapper _mapper;
        private readonly IWorkflowTaskService _WorkflowTaskService;
        private readonly IWorkflowAssignmentRepository _workflowAssignmentRepository;
        public WorkflowService(IWorkflowRepository repository,
            IUserRepository userRepository, IMapper mapper,
            IServiceItemRepository serviceItemRepository, ISlaRepository slaRepository,
            IWorkflowTaskService WorkflowTaskService,
            IWorkflowAssignmentRepository workflowAssignmentRepository,
            IRequestTicketRepository requestTicketRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
            _serviceItemRepository = serviceItemRepository;
            _slaRepository = slaRepository;
            _WorkflowTaskService = WorkflowTaskService;
            _workflowAssignmentRepository = workflowAssignmentRepository;
            _requestTicketRepository = requestTicketRepository;
        }

        public async Task<List<WorkflowDTO>> GetWorkflows()
        {
            var workflows = await _repository.GetWorkflows();
            //workflows.ForEach(async w => await HandleStatusInWorkflow(w));
            return workflows.Select(workflow => _mapper.Map<WorkflowDTO>(workflow)).ToList();
        }

        public async Task<WorkflowDTO> GetWorkflowById(string workflowId)
        {
            var workflow = await _repository.GetWorkflowById(workflowId);
            if(workflow == null)
            {
                throw new AppException($"Workflow with id {workflowId} not found");
            }
            //await HandleStatusInWorkflow(workflow);
            return _mapper.Map<WorkflowDTO>(workflow);
        }

        public async Task<WorkflowDTO> CreateWorkflow(CreateUpdateWorkflowDTO createUpdateWorkflowDTO)
        {
            var creator = await _userRepository.GetUserDetails(createUpdateWorkflowDTO.CreatedBy);
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
                CreateUpdateWorkflowTaskDTO createUpdateWorkflowTaskDTO = new()
                {
                    WorkflowTaskName = "Resolved Step",
                    Status = StatusWorkflowTaskEnum.Resolved.ToString(),
                    Description = "Resolved all task in workflow",
                    WorkflowId = workflow.WorkflowId
                };
                await _WorkflowTaskService.CreateWorkflowTask(createUpdateWorkflowTaskDTO, true);
            }
            return _mapper.Map<WorkflowDTO>(addedWorkflow);
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
            HandleWorkflowTaskInWorkflow(workflow);
            if(!string.IsNullOrEmpty(assignWorkflowDTO.ServiceItemId) && 
                string.IsNullOrEmpty(assignWorkflowDTO.RequestTicketId))
            {
                var serviceItem = await _serviceItemRepository.GetServiceItemById(assignWorkflowDTO.ServiceItemId);
                if(serviceItem == null)
                {
                    throw new AppException($"Service item with id {assignWorkflowDTO.ServiceItemId} not found");
                }
                serviceItem.WorkflowId = workflow.WorkflowId;
                await _serviceItemRepository.UpdateServiceItem(serviceItem);

            }
            if (string.IsNullOrEmpty(assignWorkflowDTO.ServiceItemId) &&
                !string.IsNullOrEmpty(assignWorkflowDTO.RequestTicketId))
            {
                var requestTicket = await _requestTicketRepository.GetRequestTicketById(assignWorkflowDTO.RequestTicketId);
                if (requestTicket == null)
                {
                    throw new AppException($"Service item with id {assignWorkflowDTO.RequestTicketId} not found");
                }
                if (!requestTicket.IsIncident)
                {
                    throw new AppException($"Only add workflow to incident ticket");
                }
                requestTicket.WorkflowId = workflow.WorkflowId;
                await _requestTicketRepository.UpdateRequestTicket(requestTicket);
            }
        }

        private void HandleWorkflowTaskInWorkflow(Workflow workflow)
        {
            var steps = workflow.WorkflowTasks.ToList();
            if(steps.Count == 0)
            {
                throw new AppException("Workflow don't have any steps!!");
            }
        }
    }
}
