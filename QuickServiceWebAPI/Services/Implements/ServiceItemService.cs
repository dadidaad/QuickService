using AutoMapper;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.Helpers;
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
        public ServiceItemService(IServiceItemRepository repository, IMapper mapper,
            AzureStorageConfig storageConfig, ILogger<ServiceItemService> logger,
            IServiceCategoryRepository serviceCategoryRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _storageConfig = storageConfig;
            _logger = logger;
            _serviceCategoryRepository = serviceCategoryRepository;
        }

        public List<ServiceItemDTO> GetServiceItems()
        {
            var serviceItems = _repository.GetServiceItems();
            return serviceItems.Select(serviceItem => _mapper.Map<ServiceItemDTO>(serviceItem)).ToList();
        }

        public async Task<ServiceItemDTO> GetServiceItemById(string serviceItemId)
        {
            var serviceItem = await _repository.GetServiceItemById(serviceItemId);
            return _mapper.Map<ServiceItemDTO>(serviceItem);
        }

        public async Task CreateServiceItem(CreateUpdateServiceItemDTO createUpdateServiceItemDTO)
        {
            if(_serviceCategoryRepository.GetServiceCategoryById(createUpdateServiceItemDTO.ServiceCategoryId) == null)
            {
                throw new AppException("Service category with id " + createUpdateServiceItemDTO.ServiceCategoryId + " not found");
            }

            var serviceItem = _mapper.Map<ServiceItem>(createUpdateServiceItemDTO);
            serviceItem.ServiceItemId = await GetNextId();
            string filePath = "";
            if (createUpdateServiceItemDTO.IconImage != null && CloudHelper.IsImage(createUpdateServiceItemDTO.IconImage))
            {
                filePath = await UpdateIcon(createUpdateServiceItemDTO.IconImage, serviceItem.ServiceItemId);
            }
            serviceItem.IconDisplay = filePath;
            await _repository.AddServiceItem(serviceItem);
        }

        public async Task UpdateServiceItem(string serviceItemId, CreateUpdateServiceItemDTO createUpdateServiceItemDTO)
        {
            ServiceItem serviceItem = await _repository.GetServiceItemById(serviceItemId);
            if(serviceItem == null)
            {
                throw new AppException("Service item with id " + serviceItemId + " not found");
            }
            if (_serviceCategoryRepository.GetServiceCategoryById(createUpdateServiceItemDTO.ServiceCategoryId) == null)
            {
                throw new AppException("Service category with id " + createUpdateServiceItemDTO.ServiceCategoryId + " not found");
            }
            serviceItem = _mapper.Map(createUpdateServiceItemDTO, serviceItem);
            string filePath = "";
            if (createUpdateServiceItemDTO.IconImage != null && CloudHelper.IsImage(createUpdateServiceItemDTO.IconImage))
            {
                filePath = await UpdateIcon(createUpdateServiceItemDTO.IconImage, serviceItem.ServiceItemId);
            }
            serviceItem.IconDisplay = filePath;
            await _repository.UpdateServiceItem(serviceItem);
        }

        public async Task DeleteServiceItem(string serviceItemId)
        {

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

        public async Task<string> UpdateIcon(IFormFile image, string serviceItemId)
        {
            string filePath = null;

            try
            {
                if (_storageConfig.AccountKey == string.Empty || _storageConfig.AccountName == string.Empty)
                    throw new AppException("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                if (_storageConfig.IconServiceItemContainer == string.Empty)
                    throw new AppException("Please provide a name for your service item container in the azure blob storage");

                if (CloudHelper.IsImage(image))
                {
                    if (image.Length > 0 && image.Length <= 2097152)
                    {
                        using (Stream stream = image.OpenReadStream())
                        {
                            string fileName = serviceItemId + Path.GetExtension(image.FileName);
                            filePath = await CloudHelper.UploadFileToStorage(stream, fileName, _storageConfig, _storageConfig.IconServiceItemContainer);
                        }
                    }
                    else
                    {
                        throw new AppException("File size is not valid");
                    }
                }
                else
                {
                    throw new AppException("Unsupported media format");
                }

                if (filePath != null)
                {
                    return filePath;
                }
                else
                {
                    throw new AppException("Error when try to upload image!!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new AppException("Error when try to upload image!!");
            }
        }
    }
}
