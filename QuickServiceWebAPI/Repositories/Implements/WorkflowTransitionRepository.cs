﻿using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class WorkflowTransitionRepository : IWorkflowTransitionRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<WorkflowTransitionRepository> _logger;
        public WorkflowTransitionRepository(QuickServiceContext context, ILogger<WorkflowTransitionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<WorkflowTransition?> AddWorkflowTransition(WorkflowTransition workflowTransition)
        {
            try
            {
                _context.WorkflowTransitions.Add(workflowTransition);
                await _context.SaveChangesAsync();
                return workflowTransition;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<List<WorkflowTransition>> GetWorkflowTransitionsByWorkflow(string workflowId)
        {
            try
            {
                return await _context.WorkflowTransitions
                    .Include(wt => wt.FromWorkflowTaskNavigation).Include(wt => wt.ToWorkflowTaskNavigation)
                    .Where(wt => wt.ToWorkflowTaskNavigation.WorkflowId == workflowId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateWorkflowTransition(WorkflowTransition workflowTransition)
        {
            try
            {
                _context.WorkflowTransitions.Update(workflowTransition);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteWorkflowTransition(WorkflowTransition workflowTransition)
        {
            try
            {
                _context.WorkflowTransitions.Remove(workflowTransition);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<WorkflowTransition> GetWorkflowTransitionById(string fromWorkflowTask, string toWorkflowTask)
        {
            WorkflowTransition workflowTransition = await _context.WorkflowTransitions
                .Include(wt => wt.FromWorkflowTaskNavigation).Include(wt => wt.ToWorkflowTaskNavigation)
                   .AsNoTracking().FirstOrDefaultAsync(x => x.FromWorkflowTask == fromWorkflowTask && x.ToWorkflowTask == toWorkflowTask);
            return workflowTransition;
        }

        public async Task<List<WorkflowTransition>> GetWorkflowTransitionsByFromWorkflowTask(string fromWorkflowTask)
        {
            List<WorkflowTransition> workflowTransitions = await _context.WorkflowTransitions
                .Include(wt => wt.FromWorkflowTaskNavigation).Include(wt => wt.ToWorkflowTaskNavigation)
                   .Where(x => x.FromWorkflowTask == fromWorkflowTask).ToListAsync();
            return workflowTransitions;
        }
    }
}
