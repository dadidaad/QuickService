using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly QuickServiceContext _context;
        private readonly ILogger<NotificationRepository> _logger;

        public NotificationRepository(QuickServiceContext context, ILogger<NotificationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddNotification(Notification notification)
        {
            try
            {
                await _context.Notifications.AddAsync(notification);
                int result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        public async Task DeleteNotification(Notification notification)
        {
            try
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Notification> GetLastNotification()
        {
            try
            {
                IQueryable<Notification> query = _context.Notifications;
                return await query.OrderByDescending(n => n.NotificationId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Notification>> GetNotifications(string ToUserId, List<string> groupIdList, bool isGetOnlyUnRead)
        {
            try
            {
                IQueryable<Notification> query = _context.Notifications
                    .Include(n => n.FromUser)
                    .Include(n => n.Relate)
                    .Include(n => n.ToGroup)
                    .Where(n => n.ToUserId == ToUserId || groupIdList.Contains(n.ToGroupId)).OrderByDescending(x => x.CreatedDate);
                if (isGetOnlyUnRead)
                {
                    query = query.Where(n => !n.IsRead);
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateNotification(Notification notification)
        {
            try
            {
                _context.Notifications.Update(notification);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
