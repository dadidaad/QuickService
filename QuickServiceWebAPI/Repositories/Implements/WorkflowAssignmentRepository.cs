using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;

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
                return _context.WorkflowAssignments.Include(w => w.CurrentStep).ToList();
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

        public async Task AddRangeWorkflowAssignment(List<WorkflowAssignment> workflowAssignments)
        {
            try
            {
                _context.WorkflowAssignments.AddRangeAsync(workflowAssignments);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<WorkflowAssignment> GetWorkflowAssignmentByCompositeKey(string referenceId, string currentStepId)
        {
            try
            {
                WorkflowAssignment workflowAssignment = await _context.WorkflowAssignments
                                                        .Include(a => a.CurrentStep) // Include the CurrentStep navigation property
                                                        .Include(a => a.Reference1) // Include the Reference1 navigation property
                                                        .SingleOrDefaultAsync(a => a.ReferenceId == referenceId
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

        public async Task<List<WorkflowAssignment>> GetAllCurrentWorkflowAssignments(List<string> workflowTasks, RequestTicket requestTicket)
        {
            try
            {
                List<WorkflowAssignment> workflowAssignments = await _context.WorkflowAssignments
                                                        .Include(a => a.CurrentStep) // Include the CurrentStep navigation property
                                                        .Include(a => a.Reference1) // Include the Reference1 navigation property
                                                        .Where(a => a.ReferenceId == requestTicket.RequestTicketId
                                                                           && workflowTasks.Any(ws => ws == a.CurrentStepId)).ToListAsync();
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

        //public async Task<bool> CheckAllWorkflowStepCompleted(List<WorkflowStep> workflowSteps, RequestTicket requestTicket)
        //{
        //    try
        //    {
        //        return await _context.WorkflowAssignments.Include(ws => ws.CurrentStep)
        //            .AnyAsync(ws => workflowSteps.All(wst => wst.WorkflowStepId == ws.CurrentStepId) 
        //            && !ws.IsCompleted && ws.ReferenceId == requestTicket.RequestTicketId 
        //            && ws.CurrentStep.Status != StatusWorkflowStepEnum.Resolved.ToString() );
        //        //.AsNoTracking().FirstOrDefaultAsync(x => x.WorkflowAssignmentId == workflowAssignmentId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred");
        //        throw;
        //    }
        //}

        public async Task DeleteRangeWorkflowAssignment(List<WorkflowAssignment> workflowAssignments)
        {
            try
            {
                _context.WorkflowAssignments.RemoveRange(workflowAssignments);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<List<WorkflowAssignment>> GetWorkflowAssignmentsByWorkflowTaskId(string workflowTaskId)
        {
            try
            {
                List<WorkflowAssignment> workflowAssignments = await _context.WorkflowAssignments
                                                        .Include(a => a.CurrentStep) // Include the CurrentStep navigation property
                                                        .Include(a => a.Reference1) // Include the Reference1 navigation property
                                                        .Where(a => a.CurrentStepId == workflowTaskId).ToListAsync();
                //.AsNoTracking().FirstOrDefaultAsync(x => x.WorkflowAssignmentId == workflowAssignmentId);
                return workflowAssignments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<List<WorkflowAssignment>> GetWorkflowAssignmentsByWorkflowId(string workflowId)
        {
            try
            {
                List<WorkflowAssignment> workflowAssignments = await _context.WorkflowAssignments
                                                        .Include(a => a.CurrentStep) // Include the CurrentStep navigation property
                                                        .Include(a => a.Reference1) // Include the Reference1 navigation property
                                                        .ToListAsync();
                //.AsNoTracking().FirstOrDefaultAsync(x => x.WorkflowAssignmentId == workflowAssignmentId);
                return workflowAssignments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<List<WorkflowAssignment>> GetWorkflowAssignmentsByRequestTicket(string requestTicketId)
        {
            try
            {
                List<WorkflowAssignment> workflowAssignments = await _context.WorkflowAssignments
                                                        .Include(wa => wa.Attachment)
                                                        .Include(a => a.CurrentStep).ThenInclude(c => c.Assigner)
                                                        .Include(a => a.CurrentStep).ThenInclude(c => c.Group)// Include the CurrentStep navigation property
                                                        .Where(s => s.ReferenceId == requestTicketId)// Include the Reference1 navigation property
                                                        .ToListAsync();
                //.AsNoTracking().FirstOrDefaultAsync(x => x.WorkflowAssignmentId == workflowAssignmentId);
                return workflowAssignments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
    }
}
