using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.DTOs.WorkflowStep;

namespace QuickServiceWebAPI.Services
{
    public interface ISlaService
    {
        public List<SlaDTO> GetSLAs();
        public Task<SlaDTO> GetSlaById(string slaId);
        public Task CreateSLA(CreateUpdateSlaDTO createUpdateSlaDTO);
        public Task UpdateSLA(string slaId, CreateUpdateSlaDTO createUpdateSlaDTO);
        public Task DeleteSLA(string slaId);
        public Task<string> GetNextId();
    }
}
