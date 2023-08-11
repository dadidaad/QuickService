using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;
using System.Transactions;

namespace QuickServiceWebAPI.Services.Implements
{
    public class RequestTicketService : IRequestTicketService
    {
        private readonly IRequestTicketRepository _requestTicketRepository;
        private readonly ILogger<RequestTicketService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IServiceItemRepository _serviceItemRepository;
        private readonly IAttachmentService _attachmentService;
        private readonly ISlaRepository _slaRepository;
        private readonly IWorkflowAssignmentService _workflowAssignmentService;
        public RequestTicketService(IRequestTicketRepository requestTicketRepository,
            ILogger<RequestTicketService> logger, IMapper mapper,
            IUserRepository userRepository,
            IServiceItemRepository serviceItemRepository, IAttachmentService attachmentService,
            ISlaRepository slaRepository, IWorkflowAssignmentService workflowAssignmentService)
        {
            _requestTicketRepository = requestTicketRepository;
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
            _serviceItemRepository = serviceItemRepository;
            _attachmentService = attachmentService;
            _slaRepository = slaRepository;
            _workflowAssignmentService = workflowAssignmentService;
        }

        public async Task<RequestTicketDTO> SendRequestTicket(CreateRequestTicketDTO createRequestTicketDTO)
        {
            var requester = await _userRepository.GetUserByEmail(createRequestTicketDTO.RequesterEmail);
            if (requester == null)
            {
                throw new AppException($"User with email {createRequestTicketDTO.RequesterEmail} not found");
            }
            var requestTicket = _mapper.Map<RequestTicket>(createRequestTicketDTO);
            requestTicket.RequesterId = requester.UserId;
            requestTicket.Status = StatusEnum.Open.ToString();
            requestTicket.State = StateEnum.New.ToString();
            requestTicket.RequestTicketId = await GetNextId();
            requestTicket.CreatedAt = DateTime.Now;
            bool hasWorkflow = false;
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (createRequestTicketDTO.IsIncident)
                {
                    await HandleIncidentTicket(requestTicket, createRequestTicketDTO);
                }
                else
                {
                    await HandleServiceRequestTicket(requestTicket, createRequestTicketDTO);
                    if(requestTicket.WorkflowId != null)
                    {
                        hasWorkflow = true;
                    }
                }
                requestTicket.Sla = await _slaRepository.GetSlaForRequestTicket(requestTicket);
                var requestTicketAdded = await _requestTicketRepository.AddRequestTicket(requestTicket);
                if(requestTicketAdded != null && hasWorkflow)
                {
                    List<string> sourcesWorkflowTasks = await _workflowAssignmentService.GetSourcesTasks(requestTicket.Workflow);
                    await _workflowAssignmentService.AssignWorkflow(sourcesWorkflowTasks, requestTicketAdded);
                }
                transactionScope.Complete();
                return _mapper.Map<RequestTicketDTO>(requestTicketAdded); 
            }
        }

        private const ImpactEnum DefaultImpactForIncident = ImpactEnum.Low;
        private const UrgencyEnum DefaultUrgencyForIncident = UrgencyEnum.Low;
        private async Task HandleIncidentTicket(RequestTicket requestTicket, CreateRequestTicketDTO createRequestTicketDTO)
        {
            requestTicket.Impact = DefaultImpactForIncident.ToString();
            requestTicket.Urgency = DefaultUrgencyForIncident.ToString();
            requestTicket.Priority = CalculatePriority(DefaultImpactForIncident, DefaultUrgencyForIncident).ToString();
            if (createRequestTicketDTO.Attachment != null)
            {
                requestTicket.Attachment = await _attachmentService.CreateAttachment(createRequestTicketDTO.Attachment);
            }
        }

        private const ImpactEnum DefaultImpactForService = ImpactEnum.Medium;
        private const UrgencyEnum DefaultUrgencyForService = UrgencyEnum.Medium;
        private async Task HandleServiceRequestTicket(RequestTicket requestTicket, CreateRequestTicketDTO createRequestTicketDTO)
        {
            var serviceItem = await _serviceItemRepository
                        .GetServiceItemById(createRequestTicketDTO.ServiceItemId);
            if (serviceItem == null)
            {
                throw new AppException($"Service item with id {createRequestTicketDTO.ServiceItemId} not found");
            }
            requestTicket.Impact = DefaultImpactForService.ToString();
            requestTicket.Urgency = DefaultUrgencyForService.ToString();
            requestTicket.Priority = CalculatePriority(DefaultImpactForService, DefaultUrgencyForService).ToString();
            //requestTicket.Title = $"Request for {serviceItem.ServiceItemName}";
            requestTicket.ServiceItemId = serviceItem.ServiceItemId;
            if (serviceItem.Workflow != null)
            {
                requestTicket.WorkflowId = serviceItem.WorkflowId;
            }
            if (createRequestTicketDTO.Attachment != null)
            {
                requestTicket.Attachment = await _attachmentService.CreateAttachment(createRequestTicketDTO.Attachment);
            }
        }

        private async Task<string> GetNextId()
        {
            RequestTicket lastRequestTicket = await _requestTicketRepository.GetLastRequestTicket();
            int id = 0;
            if (lastRequestTicket == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastRequestTicket.RequestTicketId) + 1;
            }
            string requestTicketId = IDGenerator.GenerateRequestTicketId(id);
            return requestTicketId;
        }

        private static PriorityEnum CalculatePriority(ImpactEnum impact, UrgencyEnum urgency)
        {
            if (impact == ImpactEnum.High && urgency == UrgencyEnum.High)
            {
                return PriorityEnum.Urgency;
            }
            else
            {
                int combinedValue = (int)impact + (int)urgency;

                if (combinedValue >= 5)
                    return PriorityEnum.High;
                else if (combinedValue >= 4)
                    return PriorityEnum.Medium;
                else
                    return PriorityEnum.Low;
            }

        }

        public Task<List<RequestTicketDTO>> GetAllListRequestTicket()
        {
            var requestTickets = _requestTicketRepository.GetRequestTickets();
            return Task.FromResult(requestTickets.Select(requestTicket => new RequestTicketDTO
            {
                IsIncident = requestTicket.IsIncident,
                RequestTicketId = requestTicket.RequestTicketId,
                Title = requestTicket.Title,
                RequesterUserEntity = _mapper.Map<UserDTO>(requestTicket.Requester),
                Status = requestTicket.Status,
                Sla = _mapper.Map<SlaDTO>(requestTicket.Sla),
                Description = requestTicket.Description,
                Priority = requestTicket.Priority,
                CreatedAt = requestTicket.CreatedAt,
                AssignedToUserEntity = _mapper.Map<UserDTO>(requestTicket.AssignedToNavigation),
            }).ToList());
        }

        public async Task<List<RequestTicketForRequesterDTO>> GetAllListRequestTicketForRequester(RequesterResquestDTO requesterResquestDTO)
        {
            var user = await _userRepository.GetUserByEmail(requesterResquestDTO.Requester);
            if (user == null)
            {
                throw new AppException($"Can not found user with email {requesterResquestDTO.Requester}");
            }
            var requestTickets = _requestTicketRepository.GetRequestTicketsForRequester(requesterResquestDTO.Requester);
            return requestTickets.Select(requestTicket => new RequestTicketForRequesterDTO
            {
                RequestTicketId = requestTicket.RequestTicketId,
                Title = requestTicket.Title,
                Status = requestTicket.Status,
                CreatedAt = requestTicket.CreatedAt,
                AssignedToUserEntity = _mapper.Map<UserDTO>(requestTicket.AssignedToNavigation),
                ServiceItemEntity = _mapper.Map<ServiceItemDTO>(requestTicket.ServiceItem),
            }).ToList();
        }

        public async Task<RequestTicketDTO> GetDetailsRequestTicket(string requestTicketId)
        {
            var requestTicket = await _requestTicketRepository.GetRequestTicketById(requestTicketId);
            if (requestTicket == null)
            {
                throw new AppException($"Request ticket with id {requestTicketId} not found");
            }
            return _mapper.Map<RequestTicket, RequestTicketDTO>(requestTicket);
        }

        public async Task<RequestTicketForRequesterDTO> GetDetailsRequestTicketForRequester(RequesterResquestDTO requesterResquestDTO)
        {
            var user = await _userRepository.GetUserByEmail(requesterResquestDTO.Requester);
            if (user == null)
            {
                throw new AppException($"Can not found user with email {requesterResquestDTO.Requester}");
            }
            var requestTicket = await _requestTicketRepository.GetRequestTicketById(requesterResquestDTO.RequestTicketId);
            if (requestTicket == null)
            {
                throw new AppException($"Request ticket with id {requesterResquestDTO.RequestTicketId} not found");
            }
            if (requestTicket.Requester.Email != requesterResquestDTO.Requester)
            {
                throw new AppException($"Request ticket with id {requesterResquestDTO.RequestTicketId} not belong to {requesterResquestDTO.Requester}");
            }
            return _mapper.Map<RequestTicket, RequestTicketForRequesterDTO>(requestTicket);
        }

        public async Task UpdateRequestTicket(UpdateRequestTicketDTO updateRequestTicketDTO)
        {
            var existingRequestTicket = await _requestTicketRepository.GetRequestTicketById(updateRequestTicketDTO.RequestTicketId);
            if (existingRequestTicket == null)
            {
                throw new AppException($"Request ticket item with id {updateRequestTicketDTO.RequestTicketId} not found");
            }
            if (existingRequestTicket.Status != updateRequestTicketDTO.Status
                && await _workflowAssignmentService.CheckRequestTicketExists(updateRequestTicketDTO.RequestTicketId)
                && _workflowAssignmentService.CheckStatusRequestTicketInStatusMapping(updateRequestTicketDTO.Status.ToEnum(StatusEnum.Open)))
            {
                throw new AppException($"Request ticket with id {updateRequestTicketDTO.RequestTicketId} already assign to a workflow and cannot update status");
            }
            if ((existingRequestTicket.Impact != updateRequestTicketDTO.Impact
                || existingRequestTicket.Urgency != updateRequestTicketDTO.Urgency)
                && existingRequestTicket.Priority == updateRequestTicketDTO.Priority)
            {
                updateRequestTicketDTO.Priority = CalculatePriority(updateRequestTicketDTO.Impact.ToEnum(ImpactEnum.Low)
                    , updateRequestTicketDTO.Urgency.ToEnum(UrgencyEnum.Low)).ToString();
            }
            var updateTicket = _mapper.Map(updateRequestTicketDTO, existingRequestTicket);
            updateTicket.LastUpdateAt = DateTime.Now;
            await _requestTicketRepository.UpdateRequestTicket(updateTicket);
        }

        public Task DeleteRequestTicket(string requestTicketId)
        {
            throw new NotImplementedException();
        }
    }
}
