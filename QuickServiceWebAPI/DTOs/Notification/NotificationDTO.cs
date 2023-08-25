namespace QuickServiceWebAPI.DTOs.Notification
{
    public class NotificationDTO
    {
        public string NotificationId { get; set; } = null!;

        public string NotificationHeader { get; set; } = null!;

        public string NotificationBody { get; set; } = null!;

        public bool IsRead { get; set; }

        public DateTime CreatedDate { get; set; }

        public string TargetUrl { get; set; } = null!;

        public string? NotificationType { get; set; }

        public string? RequestTicketId { get; set; }

        public string? RequestTicketTitle { get; set; }

        public string? GroupName { get; set; }

        public string? FromUserName { get; set; }

    }
}
