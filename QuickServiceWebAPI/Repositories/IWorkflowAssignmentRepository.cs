﻿using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IWorkflowAssignmentRepository
    {
        public List<WorkflowAssignment> GetWorkflowAssignments();
        public Task<WorkflowAssignment> GetWorkflowAssignmentByCompositeKey(string referenceId, string currentStepId);
        public Task<List<WorkflowAssignment>> GetAllCurrentWorkflowAssignments(List<string> workflowTasks, RequestTicket requestTicket);
        public Task<List<WorkflowAssignment>> GetWorkflowAssignmentsByRequestTicket(string requestTicketId);
        public Task<List<WorkflowAssignment>> GetWorkflowAssignmentsByWorkflowTaskId(string workflowTaskId);
        public Task<List<WorkflowAssignment>> GetWorkflowAssignmentsByWorkflowId(string workflowId);
        public Task AddWorkflowAssignment(WorkflowAssignment workflowAssignment);
        public Task AddRangeWorkflowAssignment(List<WorkflowAssignment> workflowAssignments);
        public Task UpdateWorkflowAssignment(WorkflowAssignment workflowAssignment);
        public Task DeleteRangeWorkflowAssignment(List<WorkflowAssignment> workflowAssignments);
        public Task DeleteWorkflowAssignment(WorkflowAssignment workflowAssignment);
        public Task<bool> CheckExistingRequestTicket(string requestTicketId);
        //public Task<bool> CheckAllWorkflowStepCompleted(List<WorkflowStep> workflowSteps, RequestTicket requestTicket);
    }
}
