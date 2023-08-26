using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class RequestTicketHistory
{
    public string RequestTicketId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime LastUpdate { get; set; }

    public string UserId { get; set; } = null!;

    public string RequestTicketHistoryId { get; set; } = null!;

    public virtual RequestTicket RequestTicket { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
