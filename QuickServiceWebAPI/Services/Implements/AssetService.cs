using AutoMapper;
using QuickServiceWebAPI.DTOs.Asset;
using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;
        public AssetService(IAssetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<AssetDTO> GetAssets()
        {
            var assets = _repository.GetAssets();
            return assets.Select(asset => _mapper.Map<AssetDTO>(asset)).ToList();
        }

        public async Task<AssetDTO> GetAssetById(string assetId)
        {
            var asset = await _repository.GetAssetById(assetId);
            return _mapper.Map<AssetDTO>(asset);
        }

        public async Task CreateAsset(CreateUpdateAssetDTO createUpdateAssetDTO)
        {
            var asset = _mapper.Map<Asset>(createUpdateAssetDTO);
            asset.AssetId = await GetNextId();
            await _repository.AddAsset(asset);
        }

        public async Task UpdateAsset(string assetId, CreateUpdateAssetDTO createUpdateAssetDTO)
        {
            Asset asset = await _repository.GetAssetById(assetId);
            if (asset == null)
            {
                throw new AppException("Asset not found");
            }
            asset = _mapper.Map(createUpdateAssetDTO, asset);
            await _repository.UpdateAsset(asset);
        }

        public async Task DeleteAsset(string assetId)
        {

        }
        public async Task<string> GetNextId()
        {
            Asset lastAsset = await _repository.GetLastAsset();
            int id = 0;
            if (lastAsset == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastAsset.AssetId) + 1;
            }
            string assetId = IDGenerator.GenerateAssetId(id);
            return assetId;
        }
    }
}
