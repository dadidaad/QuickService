using AutoMapper;
using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.DTOs.WorkflowStep;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class WorkflowStepService : IWorkflowStepService
    {
        private readonly IWorkflowStepRepository _repository;
        private readonly IMapper _mapper;
        public WorkflowStepService(IWorkflowStepRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<WorkflowStepDTO> GetWorkflowsStep()
        {
            var workflowSteps = _repository.GetWorkflowSteps();
            return workflowSteps.Select(workflowStep => _mapper.Map<WorkflowStepDTO>(workflowStep)).ToList();
        }

        public async Task<WorkflowStepDTO> GetWorkflowStepById(string workflowStepId)
        {
            var workflowStep = await _repository.GetWorkflowStepById(workflowStepId);
            return _mapper.Map<WorkflowStepDTO>(workflowStep);
        }

        public async Task CreateWorkflowStep(CreateUpdateWorkflowStepDTO createUpdateWorkflowStepDTO)
        {
            var workflowStep = _mapper.Map<WorkflowStep>(createUpdateWorkflowStepDTO);
            workflowStep.WorkflowStepId = await GetNextId();
            await _repository.AddWorkflowStep(workflowStep);
        }

        public async Task UpdateWorkflowStep(string workflowStepId, CreateUpdateWorkflowStepDTO CreateUpdateWorkflowStepDTO)
        {
            WorkflowStep workflowStep = await _repository.GetWorkflowStepById(workflowStepId);
            if (workflowStep == null)
            {
                throw new AppException("Service not found");
            }
            if (!String.IsNullOrEmpty(CreateUpdateWorkflowStepDTO.WorkflowStepName))
            {
                workflowStep.WorkflowStepName = CreateUpdateWorkflowStepDTO.WorkflowStepName;
            }
            if (!String.IsNullOrEmpty(CreateUpdateWorkflowStepDTO.ActionType))
            {
                workflowStep.ActionType = CreateUpdateWorkflowStepDTO.ActionType;
            }
            if (!String.IsNullOrEmpty(CreateUpdateWorkflowStepDTO.ActionDetails))
            {
                workflowStep.ActionDetails = CreateUpdateWorkflowStepDTO.ActionDetails;
            }
            if (!String.IsNullOrEmpty(CreateUpdateWorkflowStepDTO.WorkflowId))
            {
                workflowStep.WorkflowId = CreateUpdateWorkflowStepDTO.WorkflowId;
            }
            workflowStep = _mapper.Map<CreateUpdateWorkflowStepDTO, WorkflowStep>(CreateUpdateWorkflowStepDTO, workflowStep);
            await _repository.UpdateWorkflowStep(workflowStep);
        }

        public async Task DeleteWorkflowStep(string workflowStepId)
        {
            
        }
        public async Task<string> GetNextId()
        {
            WorkflowStep lastWorkflowStep = await _repository.GetLastWorkflowStep();
            int id = 0;
            if (lastWorkflowStep == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastWorkflowStep.WorkflowStepId) + 1;
            }
            string workflowStepId = IDGenerator.GenerateWorkflowStepId(id);
            return workflowStepId;
        }
    }
}
