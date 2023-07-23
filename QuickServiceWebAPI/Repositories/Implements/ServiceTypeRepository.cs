using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<ServiceTypeRepository> _logger;
        public ServiceTypeRepository(QuickServiceContext context, ILogger<ServiceTypeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddServiceType(ServiceType serviceType)
        {
            try
            {
                _context.ServiceTypes.Add(serviceType);
                await _context.SaveChangesAsync();
            }catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<ServiceType> GetServiceTypeById(string serviceTypeId)
        {
            try
            {
                ServiceType serviceType = await _context.ServiceTypes.AsNoTracking().FirstOrDefaultAsync(x => x.ServiceTypeId == serviceTypeId);
                return serviceType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<ServiceType> GetServiceTypes()
        {
            try
            {
                return _context.ServiceTypes.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateServiceType(ServiceType serviceType)
        {
            try
            {
                _context.ServiceTypes.Update(serviceType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteServiceType(ServiceType serviceType)
        {
            try
            {
                _context.ServiceTypes.Remove(serviceType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<ServiceType> GetLastServiceType()
        {
            try
            {
                return await _context.ServiceTypes.OrderByDescending(u => u.ServiceTypeId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

    }
}
