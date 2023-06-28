using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class BusinessHourRepository : IBusinessHourRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<BusinessHourRepository> _logger;
        public BusinessHourRepository(QuickServiceContext context, ILogger<BusinessHourRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddBusinessHour(BusinessHour businessHour)
        {
            try
            {
                _context.BusinessHours.Add(businessHour);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<BusinessHour> GetBusinessHourById(string businessHourId)
        {
            try
            {
                BusinessHour businessHour = await _context.BusinessHours.FirstOrDefaultAsync(x => x.BusinessHourId == businessHourId);
                return businessHour;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<BusinessHour> GetBusinessHours()
        {
            try
            {
                return _context.BusinessHours.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateBusinessHour(BusinessHour businessHour)
        {
            try
            {
                _context.BusinessHours.Update(businessHour);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteBusinessHour(BusinessHour businessHour)
        {
            try
            {
                _context.BusinessHours.Remove(businessHour);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<BusinessHour> GetLastBusinessHour()
        {
            try
            {
                return await _context.BusinessHours.OrderByDescending(u => u.BusinessHourId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
