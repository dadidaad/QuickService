using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IServiceItemCustomFieldRepository
    {
        public List<ServiceItemCustomField> GetServiceItemCustomFields();
        public Task AddServiceItemCustomField(ServiceItemCustomField serviceItemCustomField);
        public Task DeleteServiceItemCustomField(ServiceItemCustomField serviceItemCustomField);
        public Task DeleteServiceItemCustomFieldsByServiceItem(ServiceItem serviceItem);
        public Task DeleteServiceItemCustomFieldsByCustomField(CustomField customField);
    }
}
