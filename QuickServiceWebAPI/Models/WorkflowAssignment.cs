using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class WorkflowAssignment
{
    public string WorkflowAssignmentId { get; set; } = null!;

    public string ReferenceId { get; set; } = null!;

    public string CurrentTaskId { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public bool IsReject { get; set; }

    public string? RejectReason { get; set; }

    public DateTime? CompletedTime { get; set; }

    public string? CompleteMessage { get; set; }

    public string? AttachmentId { get; set; }

    public DateTime? DueDate { get; set; }

    public string? RejectBy { get; set; }

    public virtual Attachment? Attachment { get; set; }

    public virtual WorkflowTask CurrentTask { get; set; } = null!;

    public virtual RequestTicket Reference { get; set; } = null!;

    public virtual User? RejectByNavigation { get; set; }
}
