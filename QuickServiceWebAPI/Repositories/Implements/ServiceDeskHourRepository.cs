using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class ServiceDeskHourRepository : IServiceDeskHourRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<ServiceDeskHourRepository> _logger;
        public ServiceDeskHourRepository(QuickServiceContext context, ILogger<ServiceDeskHourRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddServiceDeskHour(ServiceDeskHour serviceDeskHour)
        {
            try
            {
                _context.ServiceDeskHours.Add(serviceDeskHour);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<ServiceDeskHour> GetServiceDeskHourById(string serviceDeskHourId)
        {
            try
            {
                ServiceDeskHour serviceDeskHour = await _context.ServiceDeskHours.Include(b => b.BusinessHour)
                                                  .AsNoTracking().FirstOrDefaultAsync(x => x.ServiceDeskHourId == serviceDeskHourId);
                return serviceDeskHour;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<ServiceDeskHour> GetServiceDeskHours()
        {
            try
            {
                return _context.ServiceDeskHours.Include(b => b.BusinessHour).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateServiceDeskHour(ServiceDeskHour serviceDeskHour)
        {
            try
            {
                _context.ServiceDeskHours.Update(serviceDeskHour);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteServiceDeskHour(ServiceDeskHour serviceDeskHour)
        {
            try
            {
                _context.ServiceDeskHours.Remove(serviceDeskHour);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<ServiceDeskHour> GetLastServiceDeskHour()
        {
            try
            {
                return await _context.ServiceDeskHours.OrderByDescending(u => u.ServiceDeskHourId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
