using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IServiceItemRepository
    {
        public List<ServiceItem> GetServiceItems();
        public Task<ServiceItem> GetServiceItemById(string serviceItemId);
        public Task AddServiceItem(ServiceItem serviceItem);
        public Task UpdateServiceItem(ServiceItem serviceItem);
        public Task DeleteServiceItem(ServiceItem serviceItem);
        public Task<ServiceItem> GetLastServiceItem();
    }
}
