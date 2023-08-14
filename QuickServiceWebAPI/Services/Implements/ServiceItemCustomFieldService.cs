using AutoMapper;
using QuickServiceWebAPI.DTOs.ServiceItemCustomField;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class ServiceItemCustomFieldService : IServiceItemCustomFieldService
    {
        private readonly IServiceItemCustomFieldRepository _repository;
        private readonly ILogger<ServiceItemCustomFieldService> _logger;
        private readonly IMapper _mapper;
        private readonly IServiceItemRepository _serviceItemRepository;
        private readonly ICustomFieldRepository _customFieldRepository;

        public ServiceItemCustomFieldService(IServiceItemCustomFieldRepository repository,
            ILogger<ServiceItemCustomFieldService> logger, IMapper mapper,
            IServiceItemRepository serviceItemRepository,
            ICustomFieldRepository customFieldRepository)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _serviceItemRepository = serviceItemRepository;
            _customFieldRepository = customFieldRepository;
        }
        public async Task AssignServiceItemCustomField(List<CreateUpdateServiceItemCustomFieldDTO> createUpdateServiceItemCustomFieldDTOs)
        {
            var serviceItemId = createUpdateServiceItemCustomFieldDTOs.FirstOrDefault()?.ServiceItemId;
            ServiceItem serviceItem = null;
            if(serviceItemId!=null) serviceItem = await _serviceItemRepository.GetServiceItemById(serviceItemId);
            if (serviceItem == null)
            {
                throw new AppException("Service item with id {serviceItemId} not found");
            }
            foreach(var createUpdateServiceItemCustomFieldDTO in createUpdateServiceItemCustomFieldDTOs)
            {
                var customField = await _customFieldRepository
                .GetCustomFieldById(createUpdateServiceItemCustomFieldDTO.CustomFieldId);
                if (customField == null) continue;

                //var serviceItemCustomField = await _repository
                //    .GetServiceItemCustomField(createUpdateServiceItemCustomFieldDTO.ServiceItemId,
                //    createUpdateServiceItemCustomFieldDTO.CustomFieldId);
                //if (serviceItemCustomField != null)
                //{
                //    throw new AppException("Service item custom field with id {serviceItemId} and {customFieldId} already existing",
                //        createUpdateServiceItemCustomFieldDTO.ServiceItemId,
                //        createUpdateServiceItemCustomFieldDTO.CustomFieldId);
                //}
                var serviceItemCustomField = _mapper.Map<ServiceItemCustomField>(createUpdateServiceItemCustomFieldDTO);
                serviceItemCustomField.CreatedTime = DateTime.Now;
                await _repository.AddServiceItemCustomField(serviceItemCustomField);
            }
            
        }

        public async Task DeleteServiceItemCustomField(DeleteServiceItemCustomFieldDTO deleteServiceItemCustomFieldDTO)
        {
            var serviceItem = await _serviceItemRepository
                .GetServiceItemById(deleteServiceItemCustomFieldDTO.ServiceItemId);
            if (serviceItem == null)
            {
                throw new AppException("Service item with id {serviceItemId} not found",
                    deleteServiceItemCustomFieldDTO.ServiceItemId);
            }
            var customField = await _customFieldRepository
                .GetCustomFieldById(deleteServiceItemCustomFieldDTO.CustomFieldId);
            if (customField == null)
            {
                throw new AppException("Custom field with id {customFieldId} not found",
                    deleteServiceItemCustomFieldDTO.CustomFieldId);
            }
            var serviceItemCustomField = await _repository
                .GetServiceItemCustomField(deleteServiceItemCustomFieldDTO.ServiceItemId,
                deleteServiceItemCustomFieldDTO.CustomFieldId);
            if (serviceItemCustomField == null)
            {
                throw new AppException("Service item custom field with id {serviceItemId} and {customFieldId} not found",
                    deleteServiceItemCustomFieldDTO.ServiceItemId,
                    deleteServiceItemCustomFieldDTO.CustomFieldId);
            }
            await _repository.DeleteServiceItemCustomField(serviceItemCustomField);
        }

        public async Task<List<ServiceItemCustomFieldDTO>> GetCustomFieldByServiceItem(string serviceItemId)
        {
            var serviceItem = await _serviceItemRepository
                .GetServiceItemById(serviceItemId);
            if (serviceItem == null)
            {
                throw new AppException("Service item with id {serviceItemId} not found",
                    serviceItemId);
            }
            List<ServiceItemCustomField> serviceItemCustomFields = await _repository.GetServiceItemCustomFieldsByServiceItem(serviceItemId);
            return _mapper.Map<List<ServiceItemCustomFieldDTO>>(serviceItemCustomFields);
        }
    }
}
