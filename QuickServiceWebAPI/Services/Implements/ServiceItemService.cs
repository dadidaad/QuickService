using AutoMapper;
using Microsoft.Extensions.Options;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class ServiceItemService : IServiceItemService
    {
        private readonly IServiceItemRepository _repository;
        private readonly ILogger<ServiceItemService> _logger;
        private readonly AzureStorageConfig _storageConfig;
        private readonly IMapper _mapper;
        private readonly IServiceCategoryRepository _serviceCategoryRepository;
        private readonly IServiceItemCustomFieldRepository _serviceItemCustomFieldRepository;
        private readonly IWorkflowRepository _workflowRepository;
        public ServiceItemService(IServiceItemRepository repository, IMapper mapper,
            IOptions<AzureStorageConfig> storageConfig, ILogger<ServiceItemService> logger,
            IServiceCategoryRepository serviceCategoryRepository,
            IServiceItemCustomFieldRepository serviceItemCustomFieldRepository,
            IWorkflowRepository workflowRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _storageConfig = storageConfig.Value;
            _logger = logger;
            _serviceCategoryRepository = serviceCategoryRepository;
            _serviceItemCustomFieldRepository = serviceItemCustomFieldRepository;
            _workflowRepository = workflowRepository;
        }

        public List<ServiceItemDTO> GetServiceItems()
        {
            var serviceItems = _repository.GetServiceItems();
            return serviceItems.Select(serviceItem => _mapper.Map<ServiceItemDTO>(serviceItem)).ToList();
        }

        public async Task<ServiceItemDTO> GetServiceItemById(string serviceItemId)
        {
            var serviceItem = await _repository.GetServiceItemById(serviceItemId);
            if (serviceItem == null)
            {
                throw new AppException("Service item with id " + serviceItemId + " not found");
            }
            return _mapper.Map<ServiceItemDTO>(serviceItem);
        }

        public async Task<ServiceItemDTO> CreateServiceItem(CreateUpdateServiceItemDTO createUpdateServiceItemDTO)
        {
            try
            {
                if (await _serviceCategoryRepository.GetServiceCategoryById(createUpdateServiceItemDTO.ServiceCategoryId) == null)
                {
                    throw new AppException("Service category with id " + createUpdateServiceItemDTO.ServiceCategoryId + " not found");
                }

                var serviceItem = _mapper.Map<ServiceItem>(createUpdateServiceItemDTO);
                serviceItem.ServiceItemId = await GetNextId();
                await _repository.AddServiceItem(serviceItem);
                return _mapper.Map<ServiceItemDTO>(serviceItem);
            }
            catch (Exception e)
            {

                throw new AppException("Service item with id " + e.Message); ;
            }
            
        }

        public async Task<ServiceItemDTO> UpdateServiceItem(string serviceItemId, CreateUpdateServiceItemDTO createUpdateServiceItemDTO)
        {
            ServiceItem serviceItem = await _repository.GetServiceItemById(serviceItemId);
            if (serviceItem == null)
            {
                throw new AppException("Service item with id " + serviceItemId + " not found");
            }
            if (await _serviceCategoryRepository.GetServiceCategoryById(createUpdateServiceItemDTO.ServiceCategoryId) == null)
            {
                throw new AppException("Service category with id " + createUpdateServiceItemDTO.ServiceCategoryId + " not found");
            }
            if (createUpdateServiceItemDTO.WorkflowId!=null)
            {
                var workFlow = await _workflowRepository.GetWorkflowById(createUpdateServiceItemDTO.WorkflowId);
                if (workFlow == null) throw new AppException("Workflow with id " + createUpdateServiceItemDTO.WorkflowId + " not found");
                else serviceItem.Workflow = workFlow;
            }
            
            serviceItem = _mapper.Map(createUpdateServiceItemDTO, serviceItem);
            await _repository.UpdateServiceItem(serviceItem);
            return _mapper.Map<ServiceItemDTO>(serviceItem);
        }

        public async Task DeleteServiceItem(string serviceItemId)
        {
            ServiceItem serviceItem = await _repository.GetServiceItemById(serviceItemId);
            if (serviceItem == null)
            {
                throw new AppException("Service item with id " + serviceItemId + " not found");
            }
            await _serviceItemCustomFieldRepository.DeleteServiceItemCustomFieldsByServiceItem(serviceItem);
            await _repository.DeleteServiceItem(serviceItem);
        }
        public async Task<string> GetNextId()
        {
            ServiceItem lastServiceItem = await _repository.GetLastServiceItem();
            int id = 0;
            if (lastServiceItem == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastServiceItem.ServiceItemId) + 1;
            }
            string seriveId = IDGenerator.GenerateServiceItemId(id);
            return seriveId;
        }
    }
}
