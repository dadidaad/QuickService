using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IWorkflowTaskRepository
    {
        public List<WorkflowTask> GetWorkflowTasks();
        public Task<WorkflowTask> GetWorkflowTaskById(string WorkflowTaskId);
        public Task AddWorkflowTask(WorkflowTask WorkflowTask);
        public Task UpdateWorkflowTask(WorkflowTask WorkflowTask);
        public Task DeleteWorkflowTask(WorkflowTask WorkflowTask);
        public Task<WorkflowTask> GetLastWorkflowTask();

    }
}
