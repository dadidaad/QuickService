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
        private readonly IMapper _mapper;
        private readonly Lazy<IWorkflowTaskService> _workflowTaskService;

        public WorkflowService(IWorkflowRepository repository, IUserRepository userRepository,
            IServiceItemRepository serviceItemRepository, IRequestTicketRepository requestTicketRepository,
            ISlaRepository slaRepository, IMapper mapper,
            Lazy<IWorkflowTaskService> workflowTaskService,
            IWorkflowAssignmentRepository workflowAssignmentRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _serviceItemRepository = serviceItemRepository;
            _requestTicketRepository = requestTicketRepository;
            _mapper = mapper;
            _workflowTaskService = workflowTaskService;
        }

        public async Task<List<WorkflowDTO>> GetWorkflows()
        {
            var workflows = await _repository.GetWorkflows();
            return workflows.Select(workflow => _mapper.Map<WorkflowDTO>(workflow)).ToList();
        }


        public async Task<WorkflowDTO> GetWorkflowById(string workflowId)
        {
            var workflow = await _repository.GetWorkflowById(workflowId);
            if (workflow == null)
            {
                throw new AppException($"Workflow with id {workflowId} not found");
            }
            //await HandleStatusInWorkflow(workflow);
            return _mapper.Map<WorkflowDTO>(workflow);
        }

        public async Task<WorkflowDTO> CreateWorkflow(CreateUpdateWorkflowDTO createUpdateWorkflowDTO)
        {
            var creator = await _userRepository.GetUserDetails(createUpdateWorkflowDTO.CreatedBy);
            if (creator == null)
            {
                throw new AppException($"User with id {createUpdateWorkflowDTO.CreatedBy} not found");
            }
            var workflow = _mapper.Map<Workflow>(createUpdateWorkflowDTO);
            workflow.WorkflowId = await GetNextId();
            workflow.Status = StatusWorkflowEnum.Published.ToString();
            workflow.CreatedAt = DateTime.Now;
            var addedWorkflow = await _repository.AddWorkflow(workflow);
            if (addedWorkflow != null)
            {
                await CreateOpenTaskForWorkflow(StatusWorkflowTaskEnum.Open, workflow.WorkflowId);
                await CreateOpenTaskForWorkflow(StatusWorkflowTaskEnum.Resolved, workflow.WorkflowId);
            }
            return _mapper.Map<WorkflowDTO>(addedWorkflow);
        }

        private async Task CreateOpenTaskForWorkflow(StatusWorkflowTaskEnum statusWorkflowTaskEnum, string workflowId)
        {
            CreateUpdateWorkflowTaskDTO createUpdateWorkflowTaskResolvedDTO = new()
            {
                WorkflowTaskName = $"{statusWorkflowTaskEnum} task",
                Status = statusWorkflowTaskEnum.ToString(),
                Description = $"{statusWorkflowTaskEnum} task in workflow",
                WorkflowId = workflowId
            };
            await _workflowTaskService.Value.CreateWorkflowTask(createUpdateWorkflowTaskResolvedDTO, true);
        }
        public async Task UpdateWorkflow(string workflowId, CreateUpdateWorkflowDTO createUpdateWorkflowDTO)
        {
            var workflow = await _repository.GetWorkflowById(workflowId);
            if (workflow == null)
            {
                throw new AppException("Workflow not found");
            }
            var user = await _userRepository.GetUserDetails(createUpdateWorkflowDTO.CreatedBy);
            if (user == null)
            {
                throw new AppException("Create by user with id " + createUpdateWorkflowDTO.CreatedBy + " not found");
            }
            workflow = _mapper.Map(createUpdateWorkflowDTO, workflow);
            workflow.LastUpdate = DateTime.Now;
            await _repository.UpdateWorkflow(workflow);
        }

        public async Task<bool> CheckStatusRequestTicketToEditWorkflowTask(string workflowId)
        {
            var workflow = await _repository.GetWorkflowById(workflowId);
            if (workflow == null)
            {
                throw new AppException($"Workflow with id {workflowId} not found");
            }
            var requestTickets = await _requestTicketRepository.GetAllRequestTicketRelatedToWorkflow(workflowId);
            if (requestTickets.Any(r => r.Status != StatusEnum.Resolved.ToString()
            || r.Status != StatusEnum.Canceled.ToString()
            || r.Status != StatusEnum.Closed.ToString()))
            {
                return false;
            }
            return true;
        }

        public async Task ToggleStatusWorkflow(string workflowId)
        {
            var workflow = await _repository.GetWorkflowById(workflowId);
            if (workflow == null)
            {
                throw new AppException($"Workflow with id {workflowId} not found");
            }
            if (!await CheckStatusRequestTicketToEditWorkflowTask(workflowId) && workflow.Status == StatusWorkflowEnum.Published.ToString())
            {
                throw new AppException("Workflow is handle in some service and can not disable");
            }
            if (workflow.Status == StatusWorkflowEnum.Published.ToString())
            {
                if(workflow.ServiceItems.Count > 0){
                    throw new AppException("Workflow still assign to some service items");    
                }
                workflow.Status = StatusWorkflowEnum.Drafted.ToString();
            }
            else
            {
                workflow.Status = StatusWorkflowEnum.Published.ToString();
            }
            await _repository.UpdateWorkflow(workflow);
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
            if (workflow.Status != StatusWorkflowEnum.Published.ToString())
            {
                throw new AppException($"Workflow with id {assignWorkflowDTO.WorkflowId} is drafted, change it to published first");
            }
            HandleWorkflowTaskInWorkflow(workflow);
            var serviceItem = await _serviceItemRepository.GetServiceItemById(assignWorkflowDTO.ServiceItemId);
            if (serviceItem == null)
            {
                throw new AppException($"Service item with id {assignWorkflowDTO.ServiceItemId} not found");
            }
            serviceItem.WorkflowId = workflow.WorkflowId;
            await _serviceItemRepository.UpdateServiceItem(serviceItem);
        }

        private void HandleWorkflowTaskInWorkflow(Workflow workflow)
        {
            var steps = workflow.WorkflowTasks.ToList();
            if (steps.Count == 2)
            {
                throw new AppException("Workflow don't have any action steps!!");
            }
        }

        public async Task RemoveWorkflowFromServiceItem(RemoveWorkflowFromServiceItemDTO removeWorkflowFromServiceItemDTO)
        {
            var workflow = await _repository.GetWorkflowById(removeWorkflowFromServiceItemDTO.WorkflowId);
            if (workflow == null)
            {
                throw new AppException("Workflow not found");
            }
            if (removeWorkflowFromServiceItemDTO.ServiceItemIdList.Except(workflow.ServiceItems.Select(u => u.ServiceItemId).ToList()).Any())
            {
                throw new AppException("Some service item that not assign to workflow");
            }
            var listServiceItemId = removeWorkflowFromServiceItemDTO.ServiceItemIdList;
            var removeServiceItemList = workflow.ServiceItems.Where(si => listServiceItemId.Any(siId => siId == si.ServiceItemId)).ToList();
            workflow.ServiceItems.RemoveAll(si => removeServiceItemList.Contains(si));
            await _repository.UpdateWorkflow(workflow);
        }
    }
}
