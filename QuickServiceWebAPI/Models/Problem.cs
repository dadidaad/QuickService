using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class Problem
{
    public string ProblemId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public string Impact { get; set; } = null!;

    public DateTime DueTime { get; set; }

    public string? GroupId { get; set; }

    public string? AssignerId { get; set; }

    public string RequesterId { get; set; } = null!;

    public string? RootCause { get; set; }

    public string? ImpactAnalysis { get; set; }

    public string? Symptoms { get; set; }

    public string? AttachmentId { get; set; }

    public virtual User? Assigner { get; set; }

    public virtual Attachment? Attachment { get; set; }

    public virtual ICollection<Change> Changes { get; set; } = new List<Change>();

    public virtual Group? Group { get; set; }

    public virtual ICollection<RequestTicket> RequestTickets { get; set; } = new List<RequestTicket>();

    public virtual User Requester { get; set; } = null!;
}
