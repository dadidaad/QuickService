using AutoMapper;
using QuickServiceWebAPI.DTOs.CustomField;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class CustomFieldService : ICustomFieldService
    {
        private readonly ICustomFieldRepository _repository;
        private readonly ILogger<CustomFieldService> _logger;
        private readonly IMapper _mapper;
        private readonly IServiceItemCustomFieldRepository _serviceItemCustomFieldRepository;

        public CustomFieldService(ICustomFieldRepository repository,
            ILogger<CustomFieldService> logger, IMapper mapper,
            IServiceItemCustomFieldRepository serviceItemCustomFieldRepository)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _serviceItemCustomFieldRepository = serviceItemCustomFieldRepository;
        }

        public async Task CreateCustomField(CreateUpdateCustomFieldDTO createUpdateCustomFieldDTO)
        {
            var customField = _mapper.Map<CustomField>(createUpdateCustomFieldDTO);
            customField.CustomFieldId = await GetNextId();
            customField.CreatedDate = DateTime.Now;
            await _repository.AddCustomField(customField);
        }

        public async Task DeleteCustomField(string customFieldID)
        {
            var customFieldExisting = await _repository.GetCustomFieldById(customFieldID);
            if (customFieldExisting == null)
            {
                throw new AppException("Can not found custom field with ID: {customFieldID}", customFieldID);
            }
            await _serviceItemCustomFieldRepository.DeleteServiceItemCustomFieldsByCustomField(customFieldExisting);
            await _repository.DeleteCustomField(customFieldExisting);
        }

        public async Task<CustomFieldDTO> GetCustomField(string customFieldID)
        {
            var customfield = await _repository.GetCustomFieldById(customFieldID);
            if (customfield == null)
            {
                throw new AppException("Can not found custom field with ID: {customFieldID}", customFieldID);
            }
            return _mapper.Map<CustomFieldDTO>(customfield);
        }

        public List<CustomFieldDTO> GetCustomFields()
        {
            var customfields = _repository.GetCustomFields();
            return customfields.Select(customfield => _mapper.Map<CustomFieldDTO>(customfield)).ToList();
        }

        public async Task UpdateCustomField(string customFieldID, CreateUpdateCustomFieldDTO createUpdateCustomFieldDTO)
        {
            var customFieldExisting = await _repository.GetCustomFieldById(customFieldID);
            if (customFieldExisting == null)
            {
                throw new AppException("Can not found custom field with ID: {customFieldID}", customFieldID);
            }
            var updateCustomField = _mapper.Map(createUpdateCustomFieldDTO, customFieldExisting);
            await _repository.UpdateCustomField(updateCustomField);
        }

        private async Task<string> GetNextId()
        {
            CustomField lastCustomField = await _repository.GetLastCustomField();
            int id = 0;
            if (lastCustomField == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastCustomField.CustomFieldId) + 1;
            }
            string customFieldId = IDGenerator.GenerateCustomFieldId(id);
            return customFieldId;
        }
    }
}
