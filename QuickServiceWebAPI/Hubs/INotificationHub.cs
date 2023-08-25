using QuickServiceWebAPI.DTOs.Notification;

namespace QuickServiceWebAPI.Hubs
{
    public interface INotificationHub
    {
        public Task ReceiveNotification(NotificationDTO notificationDTO); 
        public Task SendNormalMessage(string message);
    }
}
