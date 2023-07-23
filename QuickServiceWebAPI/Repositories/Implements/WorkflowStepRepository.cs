using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class WorkflowStepRepository : IWorkflowStepRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<WorkflowStepRepository> _logger;
        public WorkflowStepRepository(QuickServiceContext context, ILogger<WorkflowStepRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddWorkflowStep(WorkflowStep WorkflowStep)
        {
            try
            {
                _context.WorkflowSteps.Add(WorkflowStep);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<WorkflowStep> GetWorkflowStepById(string workflowStepId)
        {
            try
            {
                WorkflowStep workflowStep = await _context.WorkflowSteps.Include(w => w.Workflow).AsNoTracking().FirstOrDefaultAsync(x => x.WorkflowStepId == workflowStepId);
                return workflowStep;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<WorkflowStep> GetWorkflowSteps()
        {
            try
            {
                return _context.WorkflowSteps.Include(w => w.Workflow).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateWorkflowStep(WorkflowStep WorkflowStep)
        {
            try
            {
                _context.WorkflowSteps.Update(WorkflowStep);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteWorkflowStep(WorkflowStep WorkflowStep)
        {
            try
            {
                _context.WorkflowSteps.Remove(WorkflowStep);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<WorkflowStep> GetLastWorkflowStep()
        {
            try
            {
                return await _context.WorkflowSteps.OrderByDescending(u => u.WorkflowStepId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
