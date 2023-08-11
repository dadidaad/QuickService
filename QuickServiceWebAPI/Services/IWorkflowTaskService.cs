using QuickServiceWebAPI.DTOs.WorkflowTask;

namespace QuickServiceWebAPI.Services
{
    public interface IWorkflowTaskService
    {
        public Task<List<WorkflowTaskDTO>> GetWorkflowsTaskByWorkflow(string workflowId);
        public Task<WorkflowTaskDTO> GetWorkflowTaskById(string workflowTaskId);
        public Task CreateWorkflowTask(CreateUpdateWorkflowTaskDTO createUpdateWorkflowTaskDTO, bool AcceptResovledTask = false);
        public Task UpdateWorkflowTask(string workflowTaskId, CreateUpdateWorkflowTaskDTO createUpdateWorkflowTaskDTO);
        public Task DeleteWorkflowTask(string workflowTaskId);
    }
}
