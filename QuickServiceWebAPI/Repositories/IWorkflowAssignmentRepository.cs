using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IWorkflowAssignmentRepository
    {
        public List<WorkflowAssignment> GetWorkflowAssignments();
        public Task<WorkflowAssignment> GetWorkflowAssignmentByCompositeKey(string referenceId, string workflowId, string currentStepId);
        public Task<List<WorkflowAssignment>> GetAllCurrentWorkflowAssignments(List<WorkflowStep> workflowSteps, Workflow workflow, RequestTicket requestTicket);
        public Task AddWorkflowAssignment(WorkflowAssignment workflowAssignment);
        public Task AddRangeWorkflowAssignment(List<WorkflowAssignment> workflowAssignments);
        public Task UpdateWorkflowAssignment(WorkflowAssignment workflowAssignment);
        public Task DeleteWorkflowAssignment(WorkflowAssignment workflowAssignment);
        public Task<WorkflowAssignment> GetLastWorkflowAssignment();
        public Task<bool> CheckExistingRequestTicket(string requestTicketId);
    }
}
