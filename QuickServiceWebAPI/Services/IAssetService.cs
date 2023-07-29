using QuickServiceWebAPI.DTOs.Asset;

namespace QuickServiceWebAPI.Services
{
    public interface IAssetService
    {
        public List<AssetDTO> GetAssets();
        public Task<AssetDTO> GetAssetById(string assetId);
        public Task CreateAsset(CreateUpdateAssetDTO createUpdateAssetDTO);
        public Task UpdateAsset(string assetId, CreateUpdateAssetDTO createUpdateAssetDTO);
        public Task DeleteAsset(string assetId);
        public Task<string> GetNextId();
    }
}
