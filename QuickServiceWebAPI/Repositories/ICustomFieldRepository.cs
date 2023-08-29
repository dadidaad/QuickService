using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface ICustomFieldRepository
    {
        public List<CustomField> GetCustomFields();
        public Task<CustomField> GetCustomFieldById(string customFieldId);
        public Task AddCustomField(CustomField customField);
        public Task UpdateCustomField(CustomField customField);
        public Task DeleteCustomField(CustomField customField);
        public Task<CustomField> GetLastCustomField();
    }
}
