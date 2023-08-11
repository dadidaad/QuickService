using AutoMapper;
using QuickServiceWebAPI.DTOs.WorkflowTransition;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class WorkflowTransitionService : IWorkflowTransitionService
    {
        private readonly IWorkflowTransitionRepository _repository;
        private readonly IWorkflowTaskRepository _taskRepository;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMapper _mapper;

        public WorkflowTransitionService(IWorkflowTransitionRepository repository, 
            IWorkflowTaskRepository taskRepository, IMapper mapper,
            IWorkflowRepository workflowRepository)
        {
            _repository = repository;
            _taskRepository = taskRepository;
            _mapper = mapper;
            _workflowRepository = workflowRepository;
        }

        public async Task CreateWorkflowTransition(CreateWorkflowTransitionDTO workflowTransitionDTO)
        {
            var fromWorkflowTask = await _taskRepository.GetWorkflowTaskById(workflowTransitionDTO.FromWorkflowTask);
            if(fromWorkflowTask == null)
            {
                throw new AppException($"Workflow task with id {workflowTransitionDTO.FromWorkflowTask} not found");
            }
            if(workflowTransitionDTO.FromWorkflowTask != workflowTransitionDTO.ToWorkflowTask)
            {
                var toWorkflowTask = await _taskRepository.GetWorkflowTaskById(workflowTransitionDTO.ToWorkflowTask);
                if (toWorkflowTask == null)
                {
                    throw new AppException($"Workflow task with id {workflowTransitionDTO.FromWorkflowTask} not found");
                }
            }
            var workflowTransition = _mapper.Map<WorkflowTransition>(workflowTransitionDTO);
            await _repository.AddWorkflowTransition(workflowTransition);
        }

        public async Task DeleteWorkflowTransition(DeleteWorkflowTransitionDTO deleteWorkflowTransitionDTO)
        {
            var workflowTransition = await _repository
                .GetWorkflowTransitionById(deleteWorkflowTransitionDTO.FromWorkflowTask,
                deleteWorkflowTransitionDTO.ToWorkflowTask);
            if(workflowTransition == null)
            {
                throw new AppException($"Workflow transition not found");
            }
            await _repository.DeleteWorkflowTransition(workflowTransition);
        }

        public async Task<List<WorkflowTransitionDTO>> GetWorkflowTransitionByFromTaskId(string fromWorkflowTaskId)
        {
            var fromWorkflowTask = await _taskRepository.GetWorkflowTaskById(fromWorkflowTaskId);
            if (fromWorkflowTask == null)
            {
                throw new AppException($"Workflow task with id {fromWorkflowTaskId} not found");
            }
            var workflowTransitions = await _repository.GetWorkflowTransitionsByFromWorkflowTask(fromWorkflowTaskId);
            return workflowTransitions.Select(wt => _mapper.Map<WorkflowTransitionDTO>(wt)).ToList();
        }

        public async Task<List<WorkflowTransitionDTO>> GetWorkflowTransitionsByWorkflow(string workflowId)
        {
            var workflow = await _workflowRepository.GetWorkflowById(workflowId);
            if(workflow == null)
            {
                throw new AppException($"Workflow with id {workflowId} not found");
            }
            var workflowTransitions = await _repository.GetWorkflowTransitionsByWorkflow(workflow.WorkflowId);
            return workflowTransitions.Select(wt => _mapper.Map<WorkflowTransitionDTO>(wt)).ToList();
        }
    }
}
