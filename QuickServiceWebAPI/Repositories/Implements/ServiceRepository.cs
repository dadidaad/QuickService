using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<ServiceRepository> _logger;
        public ServiceRepository(QuickServiceContext context, ILogger<ServiceRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddService(Service service)
        {
            try
            {
                _context.Services.Add(service);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Service> GetServiceById(string serviceId)
        {
            try
            {
                Service service = await _context.Services.AsNoTracking().FirstOrDefaultAsync(x => x.ServiceId == serviceId);
                return service;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<Service> GetServices()
        {
            try
            {
                return _context.Services.Include(u => u.CreatedByNavigation).Include(g => g.ManagedByGroupNavigation)
                        .Include(u => u.ManagedByNavigation).Include(s => s.ServiceType).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateService(Service service)
        {
            try
            {
                _context.Services.Update(service);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteService(Service service)
        {
            try
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Service> GetLastService()
        {
            try
            {
                return await _context.Services.OrderByDescending(u => u.ServiceId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
