using AutoMapper;
using Microsoft.Extensions.Options;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
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
        private readonly IWorkflowAssignmentRepository _workflowAssignmentRepository;
        private readonly IRequestTicketRepository _requestTicketRepository;
        private readonly IAttachmentService _attachmentService;
        public ServiceItemService(IServiceItemRepository repository, IMapper mapper,
            IOptions<AzureStorageConfig> storageConfig, ILogger<ServiceItemService> logger,
            IServiceCategoryRepository serviceCategoryRepository,
            IServiceItemCustomFieldRepository serviceItemCustomFieldRepository,
            IWorkflowRepository workflowRepository, IRequestTicketRepository requestTicketRepository,
            IWorkflowAssignmentRepository workflowAssignmentRepository, IAttachmentService attachmentService)
        {
            _repository = repository;
            _mapper = mapper;
            _storageConfig = storageConfig.Value;
            _logger = logger;
            _serviceCategoryRepository = serviceCategoryRepository;
            _serviceItemCustomFieldRepository = serviceItemCustomFieldRepository;
            _workflowRepository = workflowRepository;
            _requestTicketRepository = requestTicketRepository;
            _workflowAssignmentRepository = workflowAssignmentRepository;
            _attachmentService = attachmentService;
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
            var serviceCategory = _serviceCategoryRepository.GetServiceCategoryById(createUpdateServiceItemDTO.ServiceCategoryId);
            if (serviceCategory == null)
            {
                throw new AppException("Service category with id " + createUpdateServiceItemDTO.ServiceCategoryId + " not found");
            }
            var serviceItem = _mapper.Map<ServiceItem>(createUpdateServiceItemDTO);
            serviceItem.ServiceItemId = await GetNextId();
            await _repository.AddServiceItem(serviceItem);
            return _mapper.Map<ServiceItemDTO>(serviceItem);
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
            if (createUpdateServiceItemDTO.WorkflowId != null)
            {
                var workFlow = await _workflowRepository.GetWorkflowById(createUpdateServiceItemDTO.WorkflowId);
                if (workFlow == null)
                {
                    throw new AppException("Workflow with id " + createUpdateServiceItemDTO.WorkflowId + " not found");
                }
                await HandleEditAction(serviceItem.ServiceItemId);
                serviceItem.Workflow = workFlow;
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
            await HandleEditAction(serviceItem.ServiceItemId);
            var listAttachmnetId = await _workflowAssignmentRepository.DeleteWorkflowAssignmentRelatedToServiceItem(serviceItemId);
            await _requestTicketRepository.DeleteRequestTicketRelatedToServiceItem(serviceItemId);
            await _serviceItemCustomFieldRepository.DeleteServiceItemCustomFieldsByServiceItem(serviceItem);
            await _repository.DeleteServiceItem(serviceItem);

            if (listAttachmnetId.Any())
            {
                foreach (var attachmentId in listAttachmnetId.ToHashSet())
                {
                    await _attachmentService.DeleteAttachment(attachmentId);
                }
            }
        }


        private async Task HandleEditAction(string serviceItemId)
        {
            var requestTickets = await _requestTicketRepository.GetAllRequestTicketRelatedToServiceItem(serviceItemId);
            if (requestTickets.Any(r => r.Status != StatusEnum.Resolved.ToString()
            || r.Status != StatusEnum.Canceled.ToString()
            || r.Status != StatusEnum.Closed.ToString()))
            {
                throw new AppException("Can not edit service item related to working request ticket");
            }
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
