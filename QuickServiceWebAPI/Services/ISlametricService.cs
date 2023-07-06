using QuickServiceWebAPI.DTOs.SLAMetric;

namespace QuickServiceWebAPI.Services
{
    public interface ISlametricService
    {
        public List<SlametricDTO> GetSLAmetrics();
        public Task<SlametricDTO> GetSLAmetricById(string slametricId);
        public Task CreateSLAmetric(CreateUpdateSlametricDTO createUpdateSlametricDTO);
        public Task UpdateSLAmetric(string slametricId, CreateUpdateSlametricDTO createUpdateSlametricDTO);
        public Task DeleteSLAmetric(string slametricId);
        public Task<string> GetNextId();
    }
}
