using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class SlametricRepository : ISlametricRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<SlametricRepository> _logger;
        public SlametricRepository(QuickServiceContext context, ILogger<SlametricRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddSLAmetric(Slametric slametric)
        {
            try
            {
                _context.Slametrics.Add(slametric);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Slametric> GetSLAmetricById(string slametricId)
        {
            try
            {
                Slametric slametric = await _context.Slametrics.Include(b => b.BusinessHour)
                                     .Include(s => s.Sla).FirstOrDefaultAsync(x => x.SlametricId == slametricId);
                return slametric;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<Slametric> GetSLAmetrics()
        {
            try
            {
                return _context.Slametrics.Include(b => b.BusinessHour).Include(s => s.Sla).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateSLAmetric(Slametric slametric)
        {
            try
            {
                _context.Slametrics.Update(slametric);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteSLAmetric(Slametric slametric)
        {
            try
            {
                _context.Slametrics.Remove(slametric);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Slametric> GetLastSLAmetric()
        {
            try
            {
                return await _context.Slametrics.OrderByDescending(u => u.SlametricId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
