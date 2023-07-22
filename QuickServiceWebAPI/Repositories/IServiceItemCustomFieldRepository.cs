using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IServiceItemCustomFieldRepository
    {
        public Task<ServiceItemCustomField> GetServiceItemCustomField(string serviceItemId, string customFieldId);
        public List<ServiceItemCustomField> GetServiceItemCustomFields();
        public Task<List<ServiceItemCustomField>> GetServiceItemCustomFieldsByServiceItem(string serviceItemId);
        public Task AddServiceItemCustomField(ServiceItemCustomField serviceItemCustomField);
        public Task DeleteServiceItemCustomField(ServiceItemCustomField serviceItemCustomField);
        public Task DeleteServiceItemCustomFieldsByServiceItem(ServiceItem serviceItem);
        public Task DeleteServiceItemCustomFieldsByCustomField(CustomField customField);
    }
}
