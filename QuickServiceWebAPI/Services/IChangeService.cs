using QuickServiceWebAPI.DTOs.Change;

namespace QuickServiceWebAPI.Services
{
    public interface IChangeService
    {
        public Task CreateChange(CreateChangeDTO createChangeDTO);
        public Task UpdateChange(UpdateChangeDTO updateChangeDTO);
        public Task UpdateChangeProperties(UpdateChangePropertiesDTO updateChangePropertiesDTO);
        public Task<List<ChangeDTO>> GetAllChanges();
        public Task<ChangeDTO> GetChange(string changeId);
        public Task DeleteChange(string changeId);
    }
}
