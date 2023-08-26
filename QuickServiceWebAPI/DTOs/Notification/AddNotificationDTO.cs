using QuickServiceWebAPI.Models.Enums;

namespace QuickServiceWebAPI.DTOs.Notification
{
    public class AddNotificationDTO
    {

        public string? FromUserId { get; set; }
        public string? ToUserId { get; set; }
        public string? ToGroupId { get; set; }
        public string? RelateId { get; set; }
        public NotificationTypeEnum NotificationType { get; set; }
    }
}
