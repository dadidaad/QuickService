using QuickServiceWebAPI.DTOs.ServiceItem;

namespace QuickServiceWebAPI.Services
{
    public interface IServiceItemService
    {
        public List<ServiceItemDTO> GetServiceItems();
        public Task<ServiceItemDTO> GetServiceItemById(string serviceItemId);
        public Task<ServiceItemDTO> CreateServiceItem(CreateUpdateServiceItemDTO createUpdateServiceItemDTO);
        public Task<ServiceItemDTO> UpdateServiceItem(string serviceItemId, CreateUpdateServiceItemDTO createUpdateServiceItemDTO);
        public Task DeleteServiceItem(string serviceItemId);
        public Task<string> GetNextId();
    }
}
