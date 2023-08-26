using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IServiceItemRepository
    {
        public List<ServiceItem> GetServiceItems(bool forRequester);
        public Task<ServiceItem> GetServiceItemById(string serviceItemId);
        public Task<ServiceItem> AddServiceItem(ServiceItem serviceItem);
        public Task UpdateServiceItem(ServiceItem serviceItem);
        public Task DeleteServiceItem(ServiceItem serviceItem);
        public Task<ServiceItem> GetLastServiceItem();
    }
}
