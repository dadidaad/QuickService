using QuickServiceWebAPI.DTOs.ServiceDeskHour;

namespace QuickServiceWebAPI.Services
{
    public interface IServiceDeskHourService
    {
        public List<ServiceDeskHourDTO> GetServiceDeskHours();
        public Task<ServiceDeskHourDTO> GetServiceDeskHourById(string serviceDeskHourId);
        public Task CreateServiceDeskHour(CreateUpdateServiceDeskHourDTO createUpdateServiceDeskHourDTO);
        public Task UpdateServiceDeskHour(string serviceDeskHourId, CreateUpdateServiceDeskHourDTO createUpdateServiceDeskHourDTO);
        public Task DeleteServiceDeskHour(string serviceDeskHourId);
        public Task<string> GetNextId();
    }
}
