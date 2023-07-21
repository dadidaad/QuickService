using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IAssetAssignmentRepository
    {
        public List<AssetAssignment> GetAssetAssignments();
        public Task<AssetAssignment> GetAssetAssignmentById(string assetAssignmentId);
        public Task AddAssetAssignment(AssetAssignment assetAssignment);
        public Task UpdateAssetAssignment(AssetAssignment assetAssignment);
        public Task DeleteAssetAssignment(AssetAssignment assetAssignment);
        public Task<AssetAssignment> GetLastAssetAssignment();
    }
}
