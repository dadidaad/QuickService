using QuickServiceWebAPI.DTOs.ServiceItemCustomField;

namespace QuickServiceWebAPI.Services
{
    public interface IServiceItemCustomFieldService
    {
        public Task AssignServiceItemCustomField(CreateUpdateServiceItemCustomFieldDTO createUpdateServiceItemCustomFieldDTO);
        public Task DeleteServiceItemCustomField(DeleteServiceItemCustomFieldDTO deleteServiceItemCustomFieldDTO);
        public Task<List<ServiceItemCustomFieldDTO>> GetCustomFieldByServiceItem(string serviceItemId);
    }
}
