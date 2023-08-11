using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IWorkflowTaskRepository
    {
        public List<WorkflowTask> GetWorkflowTasks();
        public Task<WorkflowTask> GetWorkflowTaskById(string workflowTaskId);
        public Task AddWorkflowTask(WorkflowTask workflowTask);
        public Task UpdateWorkflowTask(WorkflowTask workflowTask);
        public Task DeleteWorkflowTask(WorkflowTask workflowTask);
        public Task<WorkflowTask> GetLastWorkflowTask();
        public Task<List<WorkflowTask>> GetWorkflowTaskByWorkflow(string workflowId);

    }
}
