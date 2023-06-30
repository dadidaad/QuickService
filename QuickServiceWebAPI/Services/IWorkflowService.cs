using QuickServiceWebAPI.DTOs.Workflow;

namespace QuickServiceWebAPI.Services
{
    public interface IWorkflowService
    {
        public List<WorkflowDTO> GetWorkflows();
        public Task CreateWorkflow(CreateUpdateWorkflowDTO createUpdateWorkflowDTO);
        public Task UpdateWorkflow(string workflowId, CreateUpdateWorkflowDTO createUpdateWorkflowDTO);
        public Task DeleteWorkflow(string workflowId);
        public Task<string> GetNextId();
    }
}
