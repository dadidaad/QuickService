using QuickServiceWebAPI.DTOs.WorkflowTask;

namespace QuickServiceWebAPI.Services
{
    public interface IWorkflowTaskService
    {
        public Task<List<WorkflowTaskDTO>> GetWorkflowsTaskByWorkflow(string workflowId);
        public Task<WorkflowTaskDTO> GetWorkflowTaskById(string workflowTaskId);
        public Task<WorkflowTaskDTO?> CreateWorkflowTask(CreateUpdateWorkflowTaskDTO createUpdateWorkflowTaskDTO, bool AcceptResovledAndOpenTask = false);
        public Task UpdateWorkflowTask(string workflowTaskId, CreateUpdateWorkflowTaskDTO createUpdateWorkflowTaskDTO);
        public Task DeleteWorkflowTask(string workflowTaskId);
        public Task<bool> CheckConditionDeleteWorkflowTask(string workflowTaskId);
    }
}
