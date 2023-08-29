using QuickServiceWebAPI.DTOs.CustomField;

namespace QuickServiceWebAPI.Services
{
    public interface ICustomFieldService
    {
        public List<CustomFieldDTO> GetCustomFields();
        public Task CreateCustomField(CreateUpdateCustomFieldDTO createUpdateCustomFieldDTO);
        public Task<CustomFieldDTO> GetCustomField(string customFieldID);
        public Task DeleteCustomField(string customFieldID);
        public Task UpdateCustomField(string customFieldID, CreateUpdateCustomFieldDTO createUpdateCustomFieldDTO);
    }
}
