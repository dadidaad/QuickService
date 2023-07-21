using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IWorkflowAssignmentRepository
    {
        public List<WorkflowAssignment> GetWorkflowAssignments();
        public Task<WorkflowAssignment> GetWorkflowAssignmentById(string workflowAssignmentId);
        public Task AddWorkflowAssignment(WorkflowAssignment workflowAssignment);
        public Task UpdateWorkflowAssignment(WorkflowAssignment workflowAssignment);
        public Task DeleteWorkflowAssignment(WorkflowAssignment workflowAssignment);
        public Task<WorkflowAssignment> GetLastWorkflowAssignment();
    }
}
