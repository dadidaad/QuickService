using QuickServiceWebAPI.DTOs.Notification;

namespace QuickServiceWebAPI.Services
{
    public interface INotificationService
    {
        public Task<List<NotificationDTO>> GetNotifications(string ToUserId, bool isGetOnlyUnRead);
        public Task AddNotifications(AddNotificationDTO addNotificationDTO);
        public Task UpdateNotification();
        public Task DeleteNotification();
    }
}
