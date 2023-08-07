using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IWorkflowRepository
    {
        public List<Workflow> GetWorkflows();
        public Task<Workflow> GetWorkflowById(string workflowId);
        public Task AddWorkflow(Workflow Workflow);
        public Task UpdateWorkflow(Workflow Workflow);
        public Task DeleteWorkflow(Workflow Workflow);
        public Task<Workflow> GetLastWorkflow();
        public Task<int> CheckTotalOfWorkflowAssignTo(bool forServiceRequest, string? serivceItemId);
        public Task<Workflow> GetWorkflowAssignTo(bool forServiceRequest, string? serviceItemId);
    }
}
