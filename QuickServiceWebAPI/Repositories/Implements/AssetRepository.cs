using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class AssetRepository : IAssetRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<AssetRepository> _logger;
        public AssetRepository(QuickServiceContext context, ILogger<AssetRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddAsset(Asset asset)
        {
            try
            {
                _context.Assets.Add(asset);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Asset> GetAssetById(string assetId)
        {
            try
            {
                Asset asset = await _context.Assets.FirstOrDefaultAsync(x => x.AssetId == assetId);
                return asset;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<Asset> GetAssets()
        {
            try
            {
                return _context.Assets.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateAsset(Asset asset)
        {
            try
            {
                _context.Assets.Update(asset);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteAsset(Asset asset)
        {
            try
            {
                _context.Assets.Remove(asset);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Asset> GetLastAsset()
        {
            try
            {
                return await _context.Assets.OrderByDescending(u => u.AssetId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
