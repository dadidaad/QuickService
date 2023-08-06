using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class WorkflowAssignmentRepository : IWorkflowAssignmentRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<WorkflowAssignmentRepository> _logger;
        public WorkflowAssignmentRepository(QuickServiceContext context, ILogger<WorkflowAssignmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddWorkflowAssignment(WorkflowAssignment workflowAssignment)
        {
            try
            {
                _context.WorkflowAssignments.Add(workflowAssignment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<WorkflowAssignment> GetWorkflowAssignments()
        {
            try
            {
                return _context.WorkflowAssignments.Include(w => w.CurrentStep).Include(r => r.Reference1).Include(w => w.Workflow).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateWorkflowAssignment(WorkflowAssignment workflowAssignment)
        {
            try
            {
                _context.WorkflowAssignments.Update(workflowAssignment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteWorkflowAssignment(WorkflowAssignment workflowAssignment)
        {
            try
            {
                _context.WorkflowAssignments.Remove(workflowAssignment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

    }
}
