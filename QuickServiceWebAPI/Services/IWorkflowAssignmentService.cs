using QuickServiceWebAPI.DTOs.WorkflowAssignment;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;

namespace QuickServiceWebAPI.Services
{
    public interface IWorkflowAssignmentService
    {
        public Task AssignWorkflow(List<string> workflowTasks, RequestTicket requestTicket);

        public Task CompleteWorkflowTask(CheckWorkflowAssignmentDTO checkWorkflowAssignmentDTO);

        public Task<bool> CheckRequestTicketExists(string requestTicketId);
        
        public bool CheckStatusRequestTicketInStatusMapping(StatusEnum statusEnum);
        
        public Task RejectWorkflowTask(RejectWorkflowTaskDTO rejectWorkflowStepDTO);

        public Task DeleteListWorkflowAssignment(List<WorkflowAssignment> workflowAssignments);

        public Task<List<string>> GetSourcesTasks(string workflowId);

        public Task<List<WorkflowAssignmentDTO>> GetWorkflowAssignmentsForTicket(string requestTicketId);
    }
}
