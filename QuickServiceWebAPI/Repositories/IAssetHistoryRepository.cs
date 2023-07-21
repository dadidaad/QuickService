using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IAssetHistoryRepository
    {
        public List<AssetHistory> GetAssetHistories();
        public Task<AssetHistory> GetAssetHistoryById(string assetHistoryId);
        public Task AddAssetHistory(AssetHistory assetHistory);
        public Task UpdateAssetHistory(AssetHistory assetHistory);
        public Task DeleteAssetHistory(AssetHistory assetHistory);
        public Task<AssetHistory> GetLastAssetHistory();
    }
}
