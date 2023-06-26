using QuickServiceWebAPI.DTOs.Service;

namespace QuickServiceWebAPI.Services
{
    public interface IServiceService
    {
        public List<ServiceDTO> GetServices();
        public Task CreateService(CreateUpdateServiceDTO createUpdateServiceDTO);
        public Task UpdateService(string serviceId, CreateUpdateServiceDTO createUpdateServiceDTO);
        public Task DeleteService(string serviceId);
        public Task<string> GetNextId();
    }
}
