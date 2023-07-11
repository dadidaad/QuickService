using AutoMapper;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class ServiceItemService : IServiceItemService
    {
        private readonly IServiceItemRepository _repository;
        private readonly IMapper _mapper;
        public ServiceItemService(IServiceItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            var serviceItem = _mapper.Map<ServiceItem>(createUpdateServiceItemDTO);
            serviceItem.ServiceItemId = await GetNextId();
            await _repository.AddServiceItem(serviceItem);
        }

        public async Task UpdateServiceItem(string serviceItemId, CreateUpdateServiceItemDTO createUpdateServiceItemDTO)
        {
            ServiceItem serviceItem = await _repository.GetServiceItemById(serviceItemId);
            if (serviceItem == null)
            {
                throw new AppException("ServiceItem not found");
            }
            if (!String.IsNullOrEmpty(createUpdateServiceItemDTO.ServiceItemName))
            {
                serviceItem.ServiceItemName = createUpdateServiceItemDTO.ServiceItemName;
            }
            if (!String.IsNullOrEmpty(createUpdateServiceItemDTO.ShortDescription))
            {
                serviceItem.ShortDescription = createUpdateServiceItemDTO.ShortDescription;
            }
            if (!String.IsNullOrEmpty(createUpdateServiceItemDTO.Description))
            {
                serviceItem.Description = createUpdateServiceItemDTO.Description;
            }
            if (createUpdateServiceItemDTO.EstimatedDelivery > 0)
            {
                serviceItem.EstimatedDelivery = createUpdateServiceItemDTO.EstimatedDelivery;
            }
            if (!String.IsNullOrEmpty(createUpdateServiceItemDTO.Status))
            {
                serviceItem.Status = createUpdateServiceItemDTO.Status;
            }
            if (!String.IsNullOrEmpty(createUpdateServiceItemDTO.ServiceCategoryId))
            {
                serviceItem.ServiceCategoryId = createUpdateServiceItemDTO.ServiceCategoryId;
            }
            if (!String.IsNullOrEmpty(createUpdateServiceItemDTO.AttachmentId))
            {
                serviceItem.AttachmentId = createUpdateServiceItemDTO.AttachmentId;
            }
            serviceItem = _mapper.Map<CreateUpdateServiceItemDTO, ServiceItem>(createUpdateServiceItemDTO, serviceItem);
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
    }
}
