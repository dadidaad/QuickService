using QuickServiceWebAPI.DTOs.Sla;

namespace QuickServiceWebAPI.Services
{
    public interface ISlaService
    {
        public List<SlaDTO> GetSLAs();
        public Task CreateSLA(CreateUpdateSlaDTO createUpdateSlaDTO);
        public Task UpdateSLA(string slaId, CreateUpdateSlaDTO createUpdateSlaDTO);
        public Task DeleteSLA(string slaId);
        public Task<string> GetNextId();
    }
}
