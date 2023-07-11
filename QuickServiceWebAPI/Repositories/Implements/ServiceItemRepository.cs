using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class ServiceItemRepository : IServiceItemRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<ServiceItemRepository> _logger;
        public ServiceItemRepository(QuickServiceContext context, ILogger<ServiceItemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddServiceItem(ServiceItem serviceItem)
        {
            try
            {
                _context.ServiceItems.Add(serviceItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<ServiceItem> GetServiceItemById(string serviceItemId)
        {
            try
            {
                ServiceItem serviceItem = await _context.ServiceItems.Include(a => a.Attachment).Include(s => s.ServiceCategory).
                                         FirstOrDefaultAsync(x => x.ServiceItemId == serviceItemId);
                return serviceItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<ServiceItem> GetServiceItems()
        {
            try
            {
                return _context.ServiceItems.Include(a => a.Attachment).Include(s => s.ServiceCategory).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateServiceItem(ServiceItem serviceItem)
        {
            try
            {
                _context.ServiceItems.Update(serviceItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteServiceItem(ServiceItem serviceItem)
        {
            try
            {
                _context.ServiceItems.Remove(serviceItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<ServiceItem> GetLastServiceItem()
        {
            try
            {
                return await _context.ServiceItems.OrderByDescending(u => u.ServiceItemId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
