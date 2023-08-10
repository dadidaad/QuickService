using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<WorkflowRepository> _logger;
        public WorkflowRepository(QuickServiceContext context, ILogger<WorkflowRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Workflow?> AddWorkflow(Workflow workflow)
        {
            try
            {
                _context.Workflows.Add(workflow);
                await _context.SaveChangesAsync();
                var entry = _context.Entry(workflow);
                if(entry.State == EntityState.Added)
                {
                    return workflow;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Workflow> GetWorkflowById(string workflowId)
        {
            try
            {
                Workflow Workflow = await _context.Workflows.Include(w => w.WorkflowTasks)
                    .Include(u => u.CreatedByNavigation).AsNoTracking().FirstOrDefaultAsync(x => x.WorkflowId == workflowId);
                return Workflow;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<List<Workflow>> GetWorkflows()
        {
            try
            {
                return await _context.Workflows.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateWorkflow(Workflow Workflow)
        {
            try
            {
                _context.Workflows.Update(Workflow);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteWorkflow(Workflow Workflow)
        {
            try
            {
                _context.Workflows.Remove(Workflow);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Workflow> GetLastWorkflow()
        {
            try
            {
                return await _context.Workflows.OrderByDescending(u => u.WorkflowId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
