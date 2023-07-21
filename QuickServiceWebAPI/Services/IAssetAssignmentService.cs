using QuickServiceWebAPI.DTOs.AssetAssignment;
using QuickServiceWebAPI.DTOs.AssetHistory;

namespace QuickServiceWebAPI.Services
{
    public interface IAssetAssignmentService
    {
        public List<AssetAssignmentDTO> GetAssetAssignments();
        public Task<AssetAssignmentDTO> GetAssetAssignmentById(string assetAssignmentId);
        public Task CreateAssetAssignment(CreateUpdateAssetAssignmentDTO createUpdateAssetAssignmentDTO);
        public Task UpdateAssetAssignment(string assetAssignmentId, CreateUpdateAssetAssignmentDTO createUpdateAssetAssignmentDTO);
        public Task DeleteAssetAssignment(string assetAssignmentId);
        public Task<string> GetNextId();
    }
}
