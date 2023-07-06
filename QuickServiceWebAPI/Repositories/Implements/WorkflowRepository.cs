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
        public async Task AddWorkflow(Workflow Workflow)
        {
            try
            {
                _context.Workflows.Add(Workflow);
                await _context.SaveChangesAsync();
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
                Workflow Workflow = await _context.Workflows.Include(u => u.CreatedByNavigation).FirstOrDefaultAsync(x => x.WorkflowId == workflowId);
                return Workflow;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<Workflow> GetWorkflows()
        {
            try
            {
                return _context.Workflows.Include(u => u.CreatedByNavigation.Role).ToList();
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
