using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IWorkflowStepRepository
    {
        public List<WorkflowStep> GetWorkflowSteps();
        public Task<WorkflowStep> GetWorkflowStepById(string workflowStepId);
        public Task AddWorkflowStep(WorkflowStep WorkflowStep);
        public Task UpdateWorkflowStep(WorkflowStep WorkflowStep);
        public Task DeleteWorkflowStep(WorkflowStep WorkflowStep);
        public Task<WorkflowStep> GetLastWorkflowStep();
    }
}
