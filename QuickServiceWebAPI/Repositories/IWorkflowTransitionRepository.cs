using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IWorkflowTransitionRepository
    {
        public Task<List<WorkflowTransition>> GetWorkflowTransitionsByWorkflow(Workflow workflow);
        public Task<List<WorkflowTransition>> GetWorkflowTransitionsByFromWorkflowTask(string fromWorkflowTask);
        public Task<WorkflowTransition> GetWorkflowTransitionById(string fromWorkflowTask, string toWorkflowTask);
        public Task<WorkflowTransition?> AddWorkflowTransition(WorkflowTransition workflowTransititon);
        public Task UpdateWorkflowTransition(WorkflowTransition workflowTransition);
        public Task DeleteWorkflowTransition(WorkflowTransition workflowTransition);
    }
}
