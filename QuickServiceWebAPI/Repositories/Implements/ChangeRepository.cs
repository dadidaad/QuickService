using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class ChangeRepository : IChangeRepository
    {
        private readonly QuickServiceContext _context;
        private readonly ILogger<ChangeRepository> _logger;

        public ChangeRepository(QuickServiceContext context, ILogger<ChangeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddChange(Change change)
        {
            try
            {
                _context.Changes.Add(change);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Change> GetChangeById(string changeId)
        {
            try
            {
                return await _context.Changes.Include(c => c.Requester).Include(c => c.Assigner)
                    .Include(c => c.Group).Include(c => c.Attachment)
                    .AsNoTracking().FirstOrDefaultAsync(c => c.ChangeId == changeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public  async Task<List<Change>> GetChanges()
        {
            try
            {
                return  await _context.Changes.Include(c => c.Requester).Include(c => c.Assigner)
                    .Include(c => c.Group).Include(c => c.Attachment)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateChange(Change change)
        {
            try
            {
                _context.Changes.Update(change);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteChange(Change change)
        {
            try
            {
                _context.Changes.Remove(change);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Change> GetLastChange()
        {
            try
            {
                return await _context.Changes.OrderByDescending(u => u.ChangeId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
