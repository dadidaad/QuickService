using QuickServiceWebAPI.DTOs.ServiceItemCustomField;

namespace QuickServiceWebAPI.Services
{
    public interface IServiceItemCustomFieldService
    {
        public Task AssignServiceItemCustomField(List<CreateUpdateServiceItemCustomFieldDTO> createUpdateServiceItemCustomFieldDTOs);
        public Task DeleteServiceItemCustomField(DeleteServiceItemCustomFieldDTO deleteServiceItemCustomFieldDTO);
        public Task<List<ServiceItemCustomFieldDTO>> GetCustomFieldByServiceItem(string serviceItemId);
    }
}
