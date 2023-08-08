using QuickServiceWebAPI.DTOs.WorkflowAssignment;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;

namespace QuickServiceWebAPI.Services
{
    public interface IWorkflowAssignmentService
    {
        public Task AssignWorkflow(RequestTicket requestTicket);

        public Task CompleteWorkflowStep(CheckWorkflowAssignmentDTO checkWorkflowAssignmentDTO);

        public Task<bool> CheckRequestTicketExists(string requestTicketId);
        
        public bool CheckStatusRequestTicketInStatusMapping(StatusEnum statusEnum);
        
        public Task RejectWorkflowStep(RejectWorkflowStepDTO rejectWorkflowStepDTO);

        public Task DeleteListWorkflowAssignment(List<WorkflowAssignment> workflowAssignments);

        public Task<List<WorkflowAssignmentDTO>> GetWorkflowAssignmentsForTicket(string requestTicketId);
    }
}
