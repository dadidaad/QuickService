using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class SlaRepository : ISlaRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<SlaRepository> _logger;
        public SlaRepository(QuickServiceContext context, ILogger<SlaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddSLA(Sla sla)
        {
            try
            {
                _context.Slas.Add(sla);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Sla> GetSLAById(string slaId)
        {
            try
            {
                Sla sla = await _context.Slas.AsNoTracking().FirstOrDefaultAsync(x => x.Slaid == slaId);
                return sla;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<Sla> GetSLAs()
        {
            try
            {
                return _context.Slas.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateSLA(Sla sla)
        {
            try
            {
                _context.Slas.Update(sla);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteSLA(Sla sla)
        {
            try
            {
                _context.Slas.Remove(sla);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Sla> GetLastSLA()
        {
            try
            {
                return await _context.Slas.OrderByDescending(u => u.Slaid).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task<Sla> GetDefaultSla()
        {
            try
            {
                return await _context.Slas.Include(s => s.Slametrics).Where(s => s.Slaname.Contains("Default")).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
