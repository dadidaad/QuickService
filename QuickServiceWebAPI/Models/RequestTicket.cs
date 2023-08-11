using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class RequestTicket
{
    public string RequestTicketId { get; set; } = null!;

    public bool IsIncident { get; set; }

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? LastUpdateAt { get; set; }

    public DateTime? DueDate { get; set; }

    public string State { get; set; } = null!;

    public string? Tags { get; set; }

    public DateTime? ResolvedTime { get; set; }

    public string Impact { get; set; } = null!;

    public string Urgency { get; set; } = null!;

    public string RequesterId { get; set; } = null!;

    public string? ServiceItemId { get; set; }

    public string? AssignedTo { get; set; }

    public string? AssignedToGroup { get; set; }

    public string Slaid { get; set; } = null!;

    public string? AttachmentId { get; set; }

    public string Title { get; set; } = null!;

    public string? ChangeId { get; set; }

    public string? ProblemId { get; set; }

    public string? WorkflowId { get; set; }

    public virtual Group? AssignedToGroupNavigation { get; set; }

    public virtual User? AssignedToNavigation { get; set; }

    public virtual Attachment? Attachment { get; set; }

    public virtual Change? Change { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Problem? Problem { get; set; }

    public virtual ICollection<RequestTicketExt> RequestTicketExts { get; set; } = new List<RequestTicketExt>();

    public virtual User Requester { get; set; } = null!;

    public virtual ServiceItem? ServiceItem { get; set; }

    public virtual Sla Sla { get; set; } = null!;

    public virtual Workflow? Workflow { get; set; }

    public virtual ICollection<WorkflowAssignment> WorkflowAssignments { get; set; } = new List<WorkflowAssignment>();
}
