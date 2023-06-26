using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IServiceTypeRepository
    {
        public List<ServiceType> GetServiceTypes();
        public Task<ServiceType> GetServiceTypeById(string serviceTypeId);
        public Task AddServiceType(ServiceType serviceType);
        public Task UpdateServiceType(ServiceType serviceType);
        public Task DeleteServiceType(ServiceType serviceType);
        public Task<ServiceType> GetLastServiceType();

    }
}
