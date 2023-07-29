using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.DTOs.WorkflowStep;

namespace QuickServiceWebAPI.DTOs.WorkflowAssignment
{
    public class WorkflowAssignmentDTO
    {
        public string WorkflowAssignmentId { get; set; } = null!;

        public bool IsCompleted { get; set; }

        public string RequestTicketId { get; set; } = null!;

        public string WorkflowId { get; set; } = null!;

        public string CurrentStepId { get; set; } = null!;

        public virtual WorkflowStepDTO WorkflowStepEntity { get; set; } = null!;

        public virtual RequestTicketDTO RequestTicketEntity { get; set; } = null!;

        public virtual WorkflowDTO WorkflowEntity { get; set; } = null!;
    }
}
