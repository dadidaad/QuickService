using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IServiceItemCustomFieldRepository
    {
        public List<ServiceItemCustomField> GetServiceItemCustomFields();
        public Task AddServiceItemCustomField(ServiceItemCustomField serviceItemCustomField);

    }
}
