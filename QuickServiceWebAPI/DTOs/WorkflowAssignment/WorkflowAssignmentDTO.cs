using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.DTOs.WorkflowTask;

namespace QuickServiceWebAPI.DTOs.WorkflowAssignment
{
    public class WorkflowAssignmentDTO
    {
        public string WorkflowAssignmentId { get; set; } = null!;

        public bool IsCompleted { get; set; }

        public string? RejectReason { get; set; }

        public DateTime? CompletedTime { get; set; }

        public  string? Status { get; set; }

        public string? Message { get; set; }

        public DateTime? DueDate { get; set; }

        public string ReferenceId { get; set; } = null!;

        public virtual UserDTO? Assignee { get; set; }

        public virtual AttachmentDTO? Attachment { get; set; }

        public virtual WorkflowTaskDTO CurrentTask { get; set; } = null!;


    }
}
