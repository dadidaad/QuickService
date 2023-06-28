using QuickServiceWebAPI.DTOs.BusinessHour;
using QuickServiceWebAPI.DTOs.Service;

namespace QuickServiceWebAPI.Services
{
    public interface IBusinessHourService
    {
        public List<BusinessHourDTO> GetBusinessHours();
        public Task CreateBusinessHour(CreateUpdateBusinessHourDTO createUpdateBusinessHourDTO);
        public Task UpdateBusinessHour(string businessHourId, CreateUpdateBusinessHourDTO createUpdateBusinessHourDTO);
        public Task DeleteBusinessHour(string businessHourId);
        public Task<string> GetNextId();
    }
}
