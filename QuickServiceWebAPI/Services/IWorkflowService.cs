using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.DTOs.WorkflowStep;

namespace QuickServiceWebAPI.Services
{
    public interface IWorkflowService
    {
        public List<WorkflowDTO> GetWorkflows();
        public Task<WorkflowDTO> GetWorkflowById(string workflowId);
        public Task CreateWorkflow(CreateUpdateWorkflowDTO createUpdateWorkflowDTO);
        public Task UpdateWorkflow(string workflowId, CreateUpdateWorkflowDTO createUpdateWorkflowDTO);
        public Task DeleteWorkflow(string workflowId);
        public Task<string> GetNextId();
    }
}
