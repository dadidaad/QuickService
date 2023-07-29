using AutoMapper;
using QuickServiceWebAPI.DTOs.AssetHistory;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class AssetHistoryService : IAssetHistoryService
    {
        private readonly IAssetHistoryRepository _repository;
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;
        public AssetHistoryService(IAssetHistoryRepository repository, IAssetRepository assetRepository, IMapper mapper)
        {
            _repository = repository;
            _assetRepository = assetRepository;
            _mapper = mapper;
        }

        public List<AssetHistoryDTO> GetAssetHistories()
        {
            var assetHistories = _repository.GetAssetHistories();
            return assetHistories.Select(assetHistory => _mapper.Map<AssetHistoryDTO>(assetHistory)).ToList();
        }

        public async Task<AssetHistoryDTO> GetAssetHistoryById(string assetHistoryId)
        {
            var assetHistory = await _repository.GetAssetHistoryById(assetHistoryId);
            return _mapper.Map<AssetHistoryDTO>(assetHistory);
        }

        public async Task CreateAssetHistory(CreateUpdateAssetHistoryDTO createUpdateAssetHistoryDTO)
        {
            var assetHistory = _mapper.Map<AssetHistory>(createUpdateAssetHistoryDTO);
            assetHistory.AssetHistoryId = await GetNextId();
            await _repository.AddAssetHistory(assetHistory);
        }

        public async Task UpdateAssetHistory(string assetHistoryId, CreateUpdateAssetHistoryDTO createUpdateAssetHistoryDTO)
        {
            AssetHistory assetHistory = await _repository.GetAssetHistoryById(assetHistoryId);
            if (assetHistory == null)
            {
                throw new AppException("Asset history not found");
            }
            if (_assetRepository.GetAssetById(createUpdateAssetHistoryDTO.AssetId) == null)
            {
                throw new AppException("Asset with id " + createUpdateAssetHistoryDTO.AssetId + " not found");
            }
            assetHistory = _mapper.Map(createUpdateAssetHistoryDTO, assetHistory);
            await _repository.UpdateAssetHistory(assetHistory);
        }

        public async Task DeleteAssetHistory(string assetHistoryId)
        {

        }
        public async Task<string> GetNextId()
        {
            AssetHistory lastAssetHistory = await _repository.GetLastAssetHistory();
            int id = 0;
            if (lastAssetHistory == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastAssetHistory.AssetHistoryId) + 1;
            }
            string assetHistoryId = IDGenerator.GenerateAssetHistoryId(id);
            return assetHistoryId;
        }
    }
}
