using QuickServiceWebAPI.DTOs.Sla;

namespace QuickServiceWebAPI.Services
{
    public interface ISlaService
    {
        public List<SlaDTO> GetSLAs();
        public Task<SlaDTO> GetSlaById(string slaId);
        public Task<SlaDTO> CreateSLA(CreateSlaDTO createSlaDTO);
        public Task UpdateSLA(UpdateSlaDTO updateSLADTO);
        public Task DeleteSLA(string slaId);
        public Task<string> GetNextId();
    }
}
