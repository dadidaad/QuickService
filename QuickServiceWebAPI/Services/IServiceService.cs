using QuickServiceWebAPI.DTOs.Service;
using QuickServiceWebAPI.DTOs.WorkflowStep;

namespace QuickServiceWebAPI.Services
{
    public interface IServiceService
    {
        public List<ServiceDTO> GetServices();
        public Task<ServiceDTO> GetServiceById(string serviceId);
        public Task CreateService(CreateUpdateServiceDTO createUpdateServiceDTO);
        public Task UpdateService(string serviceId, CreateUpdateServiceDTO createUpdateServiceDTO);
        public Task DeleteService(string serviceId);
        public Task<string> GetNextId();
    }
}
