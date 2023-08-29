using QuickServiceWebAPI.DTOs.SLAMetric;

namespace QuickServiceWebAPI.Services
{
    public interface ISlametricService
    {
        public List<SlametricDTO> GetSLAmetrics();
        public Task<SlametricDTO> GetSLAmetricById(string slametricId);
        //public Task CreateSLAmetrics(CreateSlametricDTO createSlametricDTO);
        public Task UpdateSLAmetric(UpdateSlametricsDTO updateSlametricDTO);
        public Task DeleteSLAmetric(string slametricId);
        public Task<string> GetNextId();
    }
}
