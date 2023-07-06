using QuickServiceWebAPI.DTOs.BusinessHour;
using QuickServiceWebAPI.DTOs.Service;
using QuickServiceWebAPI.DTOs.WorkflowStep;

namespace QuickServiceWebAPI.Services
{
    public interface IBusinessHourService
    {
        public List<BusinessHourDTO> GetBusinessHours();
        public Task<BusinessHourDTO> GetBusinessHourById(string businessHourId);
        public Task CreateBusinessHour(CreateUpdateBusinessHourDTO createUpdateBusinessHourDTO);
        public Task UpdateBusinessHour(string businessHourId, CreateUpdateBusinessHourDTO createUpdateBusinessHourDTO);
        public Task DeleteBusinessHour(string businessHourId);
        public Task<string> GetNextId();
    }
}
