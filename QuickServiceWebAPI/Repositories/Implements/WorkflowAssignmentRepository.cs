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
                return _context.WorkflowAssignments.Include(w => w.CurrentStep).Include(w => w.Workflow).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateWorkflowAssignment(WorkflowAssignment workflowAssignment)
        }

        public List<WorkflowAssignment> GetWorkflowAssignments()
                return _context.WorkflowAssignments.Include(w => w.CurrentStep).Include(r => r.RequestTicket).Include(w => w.Workflow).ToList();

        public List<WorkflowAssignment> GetWorkflowAssignments()
        }

        public List<WorkflowAssignment> GetWorkflowAssignments()
        }

        public List<WorkflowAssignment> GetWorkflowAssignments()
        }

        public List<WorkflowAssignment> GetWorkflowAssignments()
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
                return await _context.WorkflowAssignments/*.OrderByDescending(u => u.WorkflowAssignmentId)*/.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
        public async Task<WorkflowAssignment> GetWorkflowAssignmentByCompositeKey(string referenceId, string workflowId, string currentStepId)
        {
            try
            {
                WorkflowAssignment workflowAssignment = await _context.WorkflowAssignments
                                                        .Include(a => a.CurrentStep) // Include the CurrentStep navigation property
                                                        .Include(a => a.Reference1) // Include the Reference1 navigation property
                                                        .Include(a => a.Workflow) // Include the Workflow navigation property
                                                        .AsNoTracking()
                                                        .SingleOrDefaultAsync(a => a.ReferenceId == referenceId
                                                                           && a.WorkflowId == workflowId
                                                                           && a.CurrentStepId == currentStepId);
                //.AsNoTracking().FirstOrDefaultAsync(x => x.WorkflowAssignmentId == workflowAssignmentId);
                return workflowAssignment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<List<WorkflowAssignment>> GetAllCurrentWorkflowAssignments(List<WorkflowStep> workflowSteps, Workflow workflow, RequestTicket requestTicket)
        {
            try
            {
                List<WorkflowAssignment> workflowAssignments = await _context.WorkflowAssignments
                                                        .Include(a => a.CurrentStep) // Include the CurrentStep navigation property
                                                        .Include(a => a.Reference1) // Include the Reference1 navigation property
                                                        .Include(a => a.Workflow) // Include the Workflow navigation property
                                                        .Where(a => a.ReferenceId == requestTicket.RequestTicketId
                                                                           && a.WorkflowId == workflow.WorkflowId
                                                                           && workflowSteps.Any(ws => ws.WorkflowStepId == a.CurrentStepId)).ToListAsync();
                //.AsNoTracking().FirstOrDefaultAsync(x => x.WorkflowAssignmentId == workflowAssignmentId);
                return workflowAssignments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<bool> CheckExistingRequestTicket(string requestTicketId)
        {
            try
            {
                return await _context.WorkflowAssignments.AnyAsync(ws => ws.ReferenceId == requestTicketId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
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
