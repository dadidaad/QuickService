using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class RequestTicketHistory
{
    public string RequestTicketHistoryId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime LastUpdate { get; set; }

    public string? RequestTicketId { get; set; }

    public string UserId { get; set; } = null!;

    public string? ChangeId { get; set; }

    public string? ProblemId { get; set; }

    public virtual Change? Change { get; set; }

    public virtual Problem? Problem { get; set; }

    public virtual RequestTicket? RequestTicket { get; set; }

    public virtual User User { get; set; } = null!;
}
