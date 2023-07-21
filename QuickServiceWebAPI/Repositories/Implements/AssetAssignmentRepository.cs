using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class AssetAssignmentRepository : IAssetAssignmentRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<AssetAssignmentRepository> _logger;
        public AssetAssignmentRepository(QuickServiceContext context, ILogger<AssetAssignmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddAssetAssignment(AssetAssignment assetAssignment)
        {
            try
            {
                _context.AssetAssignments.Add(assetAssignment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<AssetAssignment> GetAssetAssignmentById(string assetAssignmentId)
        {
            try
            {
                AssetAssignment assetAssignment = await _context.AssetAssignments.Include(a => a.Asset).Include(u => u.AssignedToNavigation).FirstOrDefaultAsync(x => x.AssetAssignmentId == assetAssignmentId);
                return assetAssignment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<AssetAssignment> GetAssetAssignments()
        {
            try
            {
                return _context.AssetAssignments.Include(a => a.Asset).Include(u => u.AssignedToNavigation).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateAssetAssignment(AssetAssignment assetAssignment)
        {
            try
            {
                _context.AssetAssignments.Update(assetAssignment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteAssetAssignment(AssetAssignment assetAssignment)
        {
            try
            {
                _context.AssetAssignments.Remove(assetAssignment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<AssetAssignment> GetLastAssetAssignment()
        {
            try
            {
                return await _context.AssetAssignments.OrderByDescending(u => u.AssetAssignmentId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
