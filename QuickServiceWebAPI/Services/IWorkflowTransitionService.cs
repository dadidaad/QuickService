using QuickServiceWebAPI.DTOs.WorkflowTransition;

namespace QuickServiceWebAPI.Services
{
    public interface IWorkflowTransitionService
    {
        public Task<List<WorkflowTransitionDTO>> GetWorkflowTransitionsByWorkflow(string workflowId);
        public Task<List<WorkflowTransitionDTO>> GetWorkflowTransitionByFromTaskId(string fromWorkflowTaskId);
        public Task CreateWorkflowTransition(WorkflowTransitionDTO workflowTransitionDTO);
        public Task DeleteWorkflowTransition(DeleteWorkflowTransitionDTO deleteWorkflowTransitionDTO);
    }
}
