using QuickServiceWebAPI.DTOs.CustomField;

namespace QuickServiceWebAPI.Services
{
    public interface ICustomFieldService
    {
        public List<CustomFieldDTO> GetCustomFields();
        public Task CreateCustomField(CreateUpdateCustomField createUpdateCustomField);
        public Task<CustomFieldDTO> GetCustomField(string customFieldID);
        public Task DeleteCustomField(string customFieldID);
        public Task UpdateCustomField(string customFieldID, CreateUpdateCustomField createUpdateCustomField);
    }
}
