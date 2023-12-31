﻿using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IWorkflowRepository
    {
        public Task<List<Workflow>> GetWorkflows();
        public Task<Workflow> GetWorkflowById(string workflowId);
        public Task<Workflow?> AddWorkflow(Workflow Workflow);
        public Task UpdateWorkflow(Workflow Workflow);
        public Task DeleteWorkflow(Workflow Workflow);
        public Task<Workflow> GetLastWorkflow();

    }
}
