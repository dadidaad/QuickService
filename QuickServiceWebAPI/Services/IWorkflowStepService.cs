using QuickServiceWebAPI.DTOs.WorkflowStep;

namespace QuickServiceWebAPI.Services
{
    public interface IWorkflowStepService
    {
        public List<WorkflowStepDTO> GetWorkflowsStep();
        public Task<WorkflowStepDTO> GetWorkflowStepById(string workflowStepId);
        public Task CreateWorkflowStep(CreateUpdateWorkflowStepDTO createUpdateWorkflowStepDTO);
        public Task UpdateWorkflowStep(string workflowStepId, CreateUpdateWorkflowStepDTO CreateUpdateWorkflowStepDTO);
        public Task DeleteWorkflowStep(string workflowStepId);
        public Task<string> GetNextId();
    }
}
