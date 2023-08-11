using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;
using System.Data;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class WorkflowTaskRepository : IWorkflowTaskRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<WorkflowTaskRepository> _logger;
        public WorkflowTaskRepository(QuickServiceContext context, ILogger<WorkflowTaskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddWorkflowTask(WorkflowTask workflowTask)
        {
            try
            {
                _context.WorkflowTasks.Add(workflowTask);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<WorkflowTask> GetWorkflowTaskById(string workflowTaskId)
        {
            try
            {
                WorkflowTask workflowTask = await _context.WorkflowTasks.Include(w => w.Workflow).AsNoTracking().FirstOrDefaultAsync(x => x.WorkflowTaskId == workflowTaskId);
                return workflowTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<WorkflowTask> GetWorkflowTasks()
        {
            try
            {
                return _context.WorkflowTasks.Include(w => w.Workflow).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateWorkflowTask(WorkflowTask workflowTask)
        {
            try
            {
                _context.WorkflowTasks.Update(workflowTask);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteWorkflowTask(WorkflowTask workflowTask)
        {
            try
            {
                await _context.Entry(workflowTask).Collection("WorkflowTransitionFromWorkflowTaskNavigations").LoadAsync();
                await _context.Entry(workflowTask).Collection("WorkflowTransitionToWorkflowTaskNavigations").LoadAsync();
                workflowTask.WorkflowTransitionFromWorkflowTaskNavigations.Clear();
                workflowTask.WorkflowTransitionToWorkflowTaskNavigations.Clear();
                _context.WorkflowTasks.Remove(workflowTask);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<WorkflowTask> GetLastWorkflowTask()
        {
            try
            {
                return await _context.WorkflowTasks.OrderByDescending(u => u.WorkflowTaskId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task<List<WorkflowTask>> GetWorkflowTaskByWorkflow(string workflowId)
        {
            try
            {
                return await _context.WorkflowTasks.Where(u => u.WorkflowId == workflowId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
