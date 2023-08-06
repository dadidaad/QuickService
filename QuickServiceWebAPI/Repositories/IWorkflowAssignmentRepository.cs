using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IWorkflowAssignmentRepository
    {
        public List<WorkflowAssignment> GetWorkflowAssignments();
        public Task AddWorkflowAssignment(WorkflowAssignment workflowAssignment);
        public Task UpdateWorkflowAssignment(WorkflowAssignment workflowAssignment);
        public Task DeleteWorkflowAssignment(WorkflowAssignment workflowAssignment);
    }
}
