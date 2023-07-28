using AutoMapper;
using QuickServiceWebAPI.DTOs.AssetAssignment;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class AssetAssignmentService : IAssetAssignmentService
    {
        private readonly IAssetAssignmentRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;
        public AssetAssignmentService(IAssetAssignmentRepository repository, IUserRepository userRepository, IAssetRepository assetRepository, IMapper mapper)
        {
            _repository = repository;
            _assetRepository = assetRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public List<AssetAssignmentDTO> GetAssetAssignments()
        {
            var assetAssignments = _repository.GetAssetAssignments();
            return assetAssignments.Select(assetAssignment => _mapper.Map<AssetAssignmentDTO>(assetAssignment)).ToList();
        }

        public async Task<AssetAssignmentDTO> GetAssetAssignmentById(string assetAssignmentId)
        {
            var assetAssignment = await _repository.GetAssetAssignmentById(assetAssignmentId);
            return _mapper.Map<AssetAssignmentDTO>(assetAssignment);
        }

        public async Task CreateAssetAssignment(CreateUpdateAssetAssignmentDTO createUpdateAssetAssignmentDTO)
        {
            var assetAssignment = _mapper.Map<AssetAssignment>(createUpdateAssetAssignmentDTO);
            assetAssignment.AssetAssignmentId = await GetNextId();
            await _repository.AddAssetAssignment(assetAssignment);
        }

        public async Task UpdateAssetAssignment(string assetAssignmentId, CreateUpdateAssetAssignmentDTO createUpdateAssetAssignmentDTO)
        {
            AssetAssignment assetAssignment = await _repository.GetAssetAssignmentById(assetAssignmentId);
            if (assetAssignment == null)
            {
                throw new AppException("Asset history not found");
            }
            if (_userRepository.GetUserDetails(createUpdateAssetAssignmentDTO.AssignedTo) == null)
            {
                throw new AppException("Assigned to user with id " + createUpdateAssetAssignmentDTO.AssignedTo + " not found");
            }
            if (_assetRepository.GetAssetById(createUpdateAssetAssignmentDTO.AssetId) == null)
            {
                throw new AppException("Asset with id " + createUpdateAssetAssignmentDTO.AssetId + " not found");
            }
            assetAssignment = _mapper.Map(createUpdateAssetAssignmentDTO, assetAssignment);
            await _repository.UpdateAssetAssignment(assetAssignment);
        }

        public async Task DeleteAssetAssignment(string assetAssignmentId)
        {

        }
        public async Task<string> GetNextId()
        {
            AssetAssignment lastAssetAssignment = await _repository.GetLastAssetAssignment();
            int id = 0;
            if (lastAssetAssignment == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastAssetAssignment.AssetAssignmentId) + 1;
            }
            string assetAssignmentId = IDGenerator.GenerateAssetAssignmentId(id);
            return assetAssignmentId;
        }
    }
}
