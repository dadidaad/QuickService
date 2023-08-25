using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface INotificationRepository
    {
        public Task<List<Notification>> GetNotifications(string ToUserId, List<string> groupIdList, bool isGetOnlyUnRead);
        public Task<bool> AddNotification(Notification notification);
        public Task UpdateNotification(Notification notification);
        public Task DeleteNotification(Notification notification);
        public Task<Notification> GetLastNotification();
    }
}
