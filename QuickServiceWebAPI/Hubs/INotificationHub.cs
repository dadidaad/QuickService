using QuickServiceWebAPI.DTOs.Notification;

namespace QuickServiceWebAPI.Hubs
{
    public interface INotificationHub
    {
        public Task SendMessage(NotificationDTO notificationDTO); 
    }
}
