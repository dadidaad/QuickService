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

    public string? AssigneeId { get; set; }

    public string? AttachmentId { get; set; }

    public string Slaid { get; set; } = null!;

    public string? RootCause { get; set; }

    public string? GroupId { get; set; }

    public DateTime CreatedTime { get; set; }

    public string? RequesterId { get; set; }

    public virtual User? Assignee { get; set; }

    public virtual Attachment? Attachment { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Group? Group { get; set; }

    public virtual ICollection<RequestTicketHistory> RequestTicketHistories { get; set; } = new List<RequestTicketHistory>();

    public virtual ICollection<RequestTicket> RequestTickets { get; set; } = new List<RequestTicket>();

    public virtual User? Requester { get; set; }

    public virtual Sla Sla { get; set; } = null!;
}
