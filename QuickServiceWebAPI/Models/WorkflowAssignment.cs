﻿using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class WorkflowAssignment
{
    public string WorkflowAssignmentId { get; set; } = null!;

    public string ReferenceId { get; set; } = null!;

    public string CurrentTaskId { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public string? Message { get; set; }

    public DateTime? CompletedTime { get; set; }

    public string? AttachmentId { get; set; }

    public DateTime? DueDate { get; set; }

    public string? AssigneeId { get; set; }

    public DateTime? HandleTime { get; set; }

    public virtual User? Assignee { get; set; }

    public virtual Attachment? Attachment { get; set; }

    public virtual WorkflowTask CurrentTask { get; set; } = null!;

    public virtual RequestTicket Reference { get; set; } = null!;
}
