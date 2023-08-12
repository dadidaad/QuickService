using QuickServiceWebAPI.DTOs.WorkflowAssignment;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;

namespace QuickServiceWebAPI.Services
{
    public interface IWorkflowAssignmentService
    {
        public Task AssignWorkflow(RequestTicket requestTicket, string? currentTaskId);

        public Task CompleteWorkflowTask(CheckWorkflowAssignmentDTO checkWorkflowAssignmentDTO);

        public Task DeleteListWorkflowAssignment(List<WorkflowAssignment> workflowAssignments);

        public Task<List<WorkflowAssignmentDTO>> GetWorkflowAssignmentsForTicket(string requestTicketId);

        public Task<bool> CheckRequestTicketExists(string requestTicketId);

        public bool CheckStatusRequestTicketInStatusMapping(StatusEnum statusEnum);
    }
}
