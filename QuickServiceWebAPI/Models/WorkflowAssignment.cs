using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class WorkflowAssignment
{
    public string ReferenceId { get; set; } = null!;

    public string WorkflowId { get; set; } = null!;

    public string CurrentStepId { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public bool IsReject { get; set; }

    public string? RejectReason { get; set; }

    public DateTime? CompletedTime { get; set; }

    public string? CompleteMessage { get; set; }

    public string? AttachmentId { get; set; }

    public DateTime? DueDate { get; set; }

    public virtual Attachment? Attachment { get; set; }

    public virtual WorkflowStep CurrentStep { get; set; } = null!;

    public virtual Change Reference { get; set; } = null!;

    public virtual RequestTicket Reference1 { get; set; } = null!;

    public virtual Problem ReferenceNavigation { get; set; } = null!;

    public virtual Workflow Workflow { get; set; } = null!;
}
