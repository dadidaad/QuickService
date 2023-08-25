using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.User;

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

        public virtual RequestTicketDTO? Relate { get; set; }

        public virtual GroupDTO? ToGroup { get; set; }

        public virtual UserDTO? FromUser { get; set; }
    }
}
