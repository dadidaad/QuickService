using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class AssetHistoryRepository : IAssetHistoryRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<AssetHistoryRepository> _logger;
        public AssetHistoryRepository(QuickServiceContext context, ILogger<AssetHistoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddAssetHistory(AssetHistory assetHistory)
        {
            try
            {
                _context.AssetHistories.Add(assetHistory);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<AssetHistory> GetAssetHistoryById(string assetHistoryId)
        {
            try
            {
                AssetHistory assetHistory = await _context.AssetHistories.Include(a => a.Asset).FirstOrDefaultAsync(x => x.AssetHistoryId == assetHistoryId);
                return assetHistory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<AssetHistory> GetAssetHistories()
        {
            try
            {
                return _context.AssetHistories.Include(a => a.Asset).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateAssetHistory(AssetHistory assetHistory)
        {
            try
            {
                _context.AssetHistories.Update(assetHistory);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteAssetHistory(AssetHistory assetHistory)
        {
            try
            {
                _context.AssetHistories.Remove(assetHistory);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<AssetHistory> GetLastAssetHistory()
        {
            try
            {
                return await _context.AssetHistories.OrderByDescending(u => u.AssetHistoryId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
