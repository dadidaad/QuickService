using QuickServiceWebAPI.DTOs.ServiceType;

namespace QuickServiceWebAPI.Services
{
    public interface IServiceTypeService
    {
        public List<ServiceTypeDTO> GetServiceTypes();
        public Task<ServiceTypeDTO> GetServiceTypeById(string serviceTypeId);
        public Task CreateServiceType(CreateUpdateServiceTypeDTO createUpdateServiceTypeDTO);
        public Task UpdateServiceType(string serviceTypeId, CreateUpdateServiceTypeDTO createUpdateServiceTypeDTO);
        public Task DeleteServiceType(string serviceTypeId);
        public Task<string> GetNextId();
    }
}
