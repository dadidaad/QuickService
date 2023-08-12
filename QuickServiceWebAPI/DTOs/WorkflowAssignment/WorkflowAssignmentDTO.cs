using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.DTOs.WorkflowTask;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.DTOs.WorkflowAssignment
{
    public class WorkflowAssignmentDTO
    {

        public string ReferenceId { get; set; } = null!;

        public string CurrentTaskId { get; set; } = null!;

        public bool IsCompleted { get; set; }

        public bool IsReject { get; set; }

        public string? RejectReason { get; set; }

        public DateTime? CompletedTime { get; set; }

        public string? CompleteMessage { get; set; }

        public DateTime? DueDate { get; set; }

        public virtual AttachmentDTO? Attachment { get; set; }

        public virtual WorkflowTaskDTO CurrentTask { get; set; } = null!;


    }
}
