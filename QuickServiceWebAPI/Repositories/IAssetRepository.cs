using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IAssetRepository
    {
        public List<Asset> GetAssets();
        public Task<Asset> GetAssetById(string assetId);
        public Task AddAsset(Asset asset);
        public Task UpdateAsset(Asset asset);
        public Task DeleteAsset(Asset asset);
        public Task<Asset> GetLastAsset();
    }
}
