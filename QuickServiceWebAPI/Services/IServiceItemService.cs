using QuickServiceWebAPI.DTOs.ServiceItem;

namespace QuickServiceWebAPI.Services
{
    public interface IServiceItemService
    {
        public List<ServiceItemDTO> GetServiceItems(bool forRequester);
        public Task<ServiceItemDTO> GetServiceItemById(string serviceItemId);
        public Task<ServiceItemDTO> CreateServiceItem(CreateUpdateServiceItemDTO createUpdateServiceItemDTO);
        public Task<ServiceItemDTO> UpdateServiceItem(string serviceItemId, CreateUpdateServiceItemDTO createUpdateServiceItemDTO);
        public Task<ServiceItemDTO> ToggleStatusWorkflow(string serviceItemId);
        public Task<string> GetNextId();
    }
}
