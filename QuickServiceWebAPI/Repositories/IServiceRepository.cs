using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IServiceRepository
    {
        public List<Service> GetServices();
        public Task<Service> GetServiceById(string serviceId);
        public Task AddService(Service service);
        public Task UpdateService(Service service);
        public Task DeleteService(Service service);
        public Task<Service> GetLastService();
    }
}
