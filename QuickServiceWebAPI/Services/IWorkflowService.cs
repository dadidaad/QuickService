using QuickServiceWebAPI.DTOs.Workflow;

namespace QuickServiceWebAPI.Services
{
    public interface IWorkflowService
    {
        public List<WorkflowDTO> GetWorkflows();
        public Task<WorkflowDTO> GetWorkflowById(string workflowId);
        public Task CreateWorkflow(CreateUpdateWorkflowDTO createUpdateWorkflowDTO);
        public Task UpdateWorkflow(string workflowId, CreateUpdateWorkflowDTO createUpdateWorkflowDTO);
        public Task DeleteWorkflow(string workflowId);
        public Task AssignWorkflow(AssignWorkflowDTO assignWorkflowDTO);
        public Task<string> GetNextId();
    }
}
