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

        public async Task<WorkflowAssignment> GetWorkflowAssignmentById(string workflowAssignmentId)
        {
            try
            {
                WorkflowAssignment workflowAssignment = await _context.WorkflowAssignments
                                                    .Include(w => w.CurrentStep).Include(r => r.RequestTicket).Include(w => w.Workflow)
                                                    .FirstOrDefaultAsync(x => x.WorkflowAssignmentId == workflowAssignmentId);
                return workflowAssignment;
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
                return _context.WorkflowAssignments.Include(w => w.CurrentStep).Include(r => r.RequestTicket).Include(w => w.Workflow).ToList();
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

        public async Task<WorkflowAssignment> GetLastWorkflowAssignment()
        {
            try
            {
                return await _context.WorkflowAssignments.OrderByDescending(u => u.WorkflowAssignmentId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
