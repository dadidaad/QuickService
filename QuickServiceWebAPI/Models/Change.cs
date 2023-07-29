using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class Change
{
    public string ChangeId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string ChangeType { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public string Impact { get; set; } = null!;

    public string Risk { get; set; } = null!;

    public string? GroupId { get; set; }

    public string RequesterId { get; set; } = null!;

    public string? AssignerId { get; set; }

    public DateTime PlannedStartDate { get; set; }

    public DateTime PlannedEndDate { get; set; }

    public string? ReasonForChange { get; set; }

    public string? ImpactPlanning { get; set; }

    public string? RolloutPlan { get; set; }

    public string? BackoutPlan { get; set; }

    public string? ProblemId { get; set; }

    public string? AttachmentId { get; set; }

    public virtual User? Assigner { get; set; }

    public virtual Attachment? Attachment { get; set; }

    public virtual Group? Group { get; set; }

    public virtual Problem? Problem { get; set; }

    public virtual ICollection<RequestTicket> RequestTickets { get; set; } = new List<RequestTicket>();

    public virtual User Requester { get; set; } = null!;
}
