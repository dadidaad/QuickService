using QuickServiceWebAPI.DTOs.Workflow;

namespace QuickServiceWebAPI.Services
{
    public interface IWorkflowService
    {
        public Task<List<WorkflowDTO>> GetWorkflows();
        public Task<WorkflowDTO> GetWorkflowById(string workflowId);
        public Task<WorkflowDTO> CreateWorkflow(CreateUpdateWorkflowDTO createUpdateWorkflowDTO);
        public Task RemoveWorkflowFromServiceItem(RemoveWorkflowFromServiceItemDTO removeWorkflowFromServiceItemDTO);
        public Task UpdateWorkflow(string workflowId, CreateUpdateWorkflowDTO createUpdateWorkflowDTO);
        public Task ToggleStatusWorkflow(string workflowId);
        public Task AssignWorkflow(AssignWorkflowDTO assignWorkflowDTO);
        public Task<string> GetNextId();
        public Task<bool> CheckStatusRequestTicketToEditWorkflowTask(string workflowId);
    }
}
