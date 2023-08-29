using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IServiceDeskHourRepository
    {
        public List<ServiceDeskHour> GetServiceDeskHours();
        public Task<ServiceDeskHour> GetServiceDeskHourById(string serviceDeskHourId);
        public Task AddServiceDeskHour(ServiceDeskHour serviceDeskHour);
        public Task UpdateServiceDeskHour(ServiceDeskHour serviceDeskHour);
        public Task DeleteServiceDeskHour(ServiceDeskHour serviceDeskHour);
        public Task<ServiceDeskHour> GetLastServiceDeskHour();
    }
}
