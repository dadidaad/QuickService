using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IBusinessHourRepository
    {
        public List<BusinessHour> GetBusinessHours();
        public Task<BusinessHour> GetBusinessHourById(string businessHourId);
        public Task AddBusinessHour(BusinessHour businessHour);
        public Task UpdateBusinessHour(BusinessHour businessHour);
        public Task DeleteBusinessHour(BusinessHour businessHour);
        public Task<BusinessHour> GetLastBusinessHour();
    }
}
