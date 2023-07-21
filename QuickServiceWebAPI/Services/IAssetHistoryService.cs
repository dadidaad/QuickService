using QuickServiceWebAPI.DTOs.Asset;
using QuickServiceWebAPI.DTOs.AssetHistory;

namespace QuickServiceWebAPI.Services
{
    public interface IAssetHistoryService
    {
        public List<AssetHistoryDTO> GetAssetHistories();
        public Task<AssetHistoryDTO> GetAssetHistoryById(string assetHistoryId);
        public Task CreateAssetHistory(CreateUpdateAssetHistoryDTO createUpdateAssetHistoryDTO);
        public Task UpdateAssetHistory(string assetHistoryId, CreateUpdateAssetHistoryDTO createUpdateAssetHistoryDTO);
        public Task DeleteAssetHistory(string assetHistoryId);
        public Task<string> GetNextId();
    }
}
