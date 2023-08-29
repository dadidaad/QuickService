using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class Notification
{
    public string NotificationId { get; set; } = null!;

    public string? FromUserId { get; set; }

    public string? ToUserId { get; set; }

    public string? ToGroupId { get; set; }

    public string NotificationHeader { get; set; } = null!;

    public string NotificationBody { get; set; } = null!;

    public bool IsRead { get; set; }

    public DateTime CreatedDate { get; set; }

    public string TargetUrl { get; set; } = null!;

    public string? NotificationType { get; set; }

    public string? RelateId { get; set; }

    public virtual User? FromUser { get; set; }

    public virtual RequestTicket? Relate { get; set; }

    public virtual Group? ToGroup { get; set; }

    public virtual User? ToUser { get; set; }
}
