using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.Notification;
using QuickServiceWebAPI.DTOs.Query;
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
        private readonly IRequestTicketHistoryService _requestTicketHistoryService;
        private readonly IRequestTicketHistoryRepository _requestTicketHistoryRepository;
        private readonly IQueryRepository _queryRepository;
        private readonly INotificationService _notificationService;
        private readonly IGroupRepository _groupRepository;
        private readonly IChangeService _changeService;
        private readonly IProblemService _problemService;

        public RequestTicketService(IRequestTicketRepository requestTicketRepository,
            ILogger<RequestTicketService> logger, IMapper mapper,
            IUserRepository userRepository,
            IServiceItemRepository serviceItemRepository, IAttachmentService attachmentService,
            ISlaRepository slaRepository, IWorkflowAssignmentService workflowAssignmentService
            , IRequestTicketHistoryService requestTicketHistoryService
            , IRequestTicketHistoryRepository requestTicketHistoryRepository
            , IQueryRepository queryRepository
            , INotificationService notificationService
            , IGroupRepository groupRepository,
            IChangeService changeService,
            IProblemService problemService)
        {
            _requestTicketRepository = requestTicketRepository;
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
            _serviceItemRepository = serviceItemRepository;
            _attachmentService = attachmentService;
            _slaRepository = slaRepository;
            _workflowAssignmentService = workflowAssignmentService;
            _requestTicketHistoryService = requestTicketHistoryService;
            _requestTicketHistoryRepository = requestTicketHistoryRepository;
            _queryRepository = queryRepository;
            _notificationService = notificationService;
            _groupRepository = groupRepository;
            _changeService = changeService;
            _problemService = problemService;
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
                    if (requestTicket.WorkflowId != null)
                    {
                        hasWorkflow = true;
                    }
                }
                requestTicket.Sla = await _slaRepository.GetSlaForRequestTicket(requestTicket);
                var requestTicketAdded = await _requestTicketRepository.AddRequestTicket(requestTicket);

                var history = new RequestTicketHistory
                {

                    RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory(),
                    RequestTicketId = requestTicket.RequestTicketId,
                    Content = $"{requestTicket.Requester.FirstName} Create request",
                    LastUpdate = requestTicket.CreatedAt,
                    UserId = requestTicket.RequesterId
                };
                await _requestTicketHistoryRepository.AddRequestTicketHistory(history);

                if (requestTicketAdded != null && hasWorkflow)
                {
                    await _workflowAssignmentService.AssignWorkflow(requestTicketAdded, null);
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



        public async Task<List<RequestTicketDTO>> GetAllListRequestTicket()
        {
            var requestTickets = _requestTicketRepository.GetRequestTickets();
            return requestTickets.Select(x => _mapper.Map<RequestTicketDTO>(x)).OrderByDescending(x => x.CreatedAt).ToList();
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
                IsIncident = requestTicket.IsIncident,
                Title = requestTicket.Title,
                Status = requestTicket.Status,
                CreatedAt = requestTicket.CreatedAt,
                AssignedToUserEntity = _mapper.Map<UserDTO>(requestTicket.AssignedToNavigation),
                ServiceItemEntity = _mapper.Map<ServiceItemDTO>(requestTicket.ServiceItem),
            }).OrderByDescending(x => x.CreatedAt).ToList();
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

        public async Task<RequestTicketDTO> UpdateRequestTicket(UpdateRequestTicketDTO updateRequestTicketDTO)
        {
            var existingRequestTicket = await _requestTicketRepository.GetRequestTicketById(updateRequestTicketDTO.RequestTicketId);
            if (existingRequestTicket == null)
            {
                throw new AppException($"Request ticket item with id {updateRequestTicketDTO.RequestTicketId} not found");
            }
            if (existingRequestTicket.Status != updateRequestTicketDTO.Status && existingRequestTicket.Status != StatusEnum.Resolved.ToString()
                && await _workflowAssignmentService.CheckRequestTicketExists(updateRequestTicketDTO.RequestTicketId)
                && _workflowAssignmentService.CheckStatusRequestTicketInStatusMapping(updateRequestTicketDTO.Status.ToEnum(StatusEnum.Open)))
            {
                throw new AppException($"Request ticket with id {updateRequestTicketDTO.RequestTicketId} already assign to a workflow and cannot update status");
            }

            if (await _workflowAssignmentService.CheckRequestTicketExists(updateRequestTicketDTO.RequestTicketId) &&
                (!string.IsNullOrEmpty(updateRequestTicketDTO.AssignedTo) || !string.IsNullOrEmpty(updateRequestTicketDTO.AssignedToGroup)))
            {
                throw new AppException($"Request ticket with id {updateRequestTicketDTO.RequestTicketId} already assign to a workflow and cannot update assigner");
            }

            bool needSendNoti = false;
            if (!string.IsNullOrEmpty(updateRequestTicketDTO.AssignedTo))
            {
                if (await _userRepository.GetUserDetails(updateRequestTicketDTO.AssignedTo) == null)
                {
                    throw new AppException($"User with id {updateRequestTicketDTO.AssignedTo} not found");
                }
                needSendNoti = true;
            }

            if (!string.IsNullOrEmpty(updateRequestTicketDTO.AssignedToGroup))
            {
                if (await _groupRepository.GetGroupById(updateRequestTicketDTO.AssignedToGroup) == null)
                {
                    throw new AppException($"Group with id {updateRequestTicketDTO.AssignedTo} not found");
                }
                needSendNoti = true;
            }

            if ((existingRequestTicket.Impact != updateRequestTicketDTO.Impact
                || existingRequestTicket.Urgency != updateRequestTicketDTO.Urgency))
            {
                existingRequestTicket.Priority = CalculatePriority(updateRequestTicketDTO.Impact.ToEnum(ImpactEnum.Low)
                    , updateRequestTicketDTO.Urgency.ToEnum(UrgencyEnum.Low)).ToString();
            }

            //if (updateRequestTicketDTO.Status == StatusEnum.Resolved.ToString())
            //{
            //    var history = new RequestTicketHistory();
            //    history.Content = $"Ticket is Completed";
            //    history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
            //    history.RequestTicketId = existingRequestTicket.RequestTicketId;
            //    history.LastUpdate = DateTime.Now;
            //    history.UserId = existingRequestTicket.AssignedTo;
            //    await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
            //}

            //if (existingRequestTicket.Status != updateRequestTicketDTO.Status && updateRequestTicketDTO.Status != StatusEnum.Resolved.ToString())
            //{
            //    var history = new RequestTicketHistory();
            //    history.Content = $"{existingRequestTicket.AssignedToNavigation.FirstName} Change Status to {updateRequestTicketDTO.Status}";
            //    history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
            //    history.RequestTicketId = existingRequestTicket.RequestTicketId;
            //    history.LastUpdate = DateTime.Now;
            //    history.UserId = existingRequestTicket.AssignedTo;
            //    await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
            //}

            //if (existingRequestTicket.Impact != updateRequestTicketDTO.Impact)
            //{
            //    var history = new RequestTicketHistory();
            //    history.Content = $"{existingRequestTicket.AssignedToNavigation.FirstName} Change Impact to {updateRequestTicketDTO.Impact}";
            //    history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
            //    history.RequestTicketId = existingRequestTicket.RequestTicketId;
            //    history.LastUpdate = DateTime.Now;
            //    history.UserId = existingRequestTicket.AssignedTo;
            //    await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
            //}

            //if (existingRequestTicket.Urgency != updateRequestTicketDTO.Urgency)
            //{
            //    var history = new RequestTicketHistory();
            //    history.Content = $"{existingRequestTicket.AssignedToNavigation.FirstName} Change Urgency to {updateRequestTicketDTO.Urgency}";
            //    history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
            //    history.RequestTicketId = existingRequestTicket.RequestTicketId;
            //    history.LastUpdate = DateTime.Now;
            //    history.UserId = existingRequestTicket.AssignedTo;
            //    await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
            //}

            var updateTicket = _mapper.Map(updateRequestTicketDTO, existingRequestTicket);
            updateTicket.LastUpdateAt = DateTime.Now;
            if (updateTicket.Status == StatusEnum.Resolved.ToString())
            {
                updateTicket.ResolvedTime = DateTime.Now;
            }
            await _requestTicketRepository.UpdateRequestTicket(updateTicket);

            if (needSendNoti)
            {
                await HandleSendNotification(updateTicket);
            }
            return _mapper.Map<RequestTicketDTO>(updateTicket);
        }

        private async Task HandleSendNotification(RequestTicket requestTicket)
        {
            var notificationDto = new AddNotificationDTO();
            if (!string.IsNullOrEmpty(requestTicket.AssignedToGroup) && string.IsNullOrEmpty(requestTicket.AssignedTo))
            {
                notificationDto.ToGroupId = requestTicket.AssignedToGroup;
                notificationDto.NotificationType = NotificationTypeEnum.AssignGroup;
            }
            if (!string.IsNullOrEmpty(requestTicket.AssignedTo))
            {
                notificationDto.ToUserId = requestTicket.AssignedTo;
                notificationDto.NotificationType = NotificationTypeEnum.AssignUser;
            }
            notificationDto.RelateId = requestTicket.RequestTicketId;
            await _notificationService.AddNotifications(notificationDto);
        }

        public Task DeleteRequestTicket(string requestTicketId)
        {
            throw new NotImplementedException();
        }

        public async Task CancelRequestTicket(string requestTicketId)
        {
            var requestTicket = await _requestTicketRepository.GetRequestTicketById(requestTicketId);
            if (requestTicketId == null)
            {
                throw new AppException($"Request ticket with id {requestTicketId} not found");
            }
            requestTicket.Status = StatusEnum.Canceled.ToString();
            requestTicket.State = StateEnum.Cancel.ToString();
            await _requestTicketRepository.UpdateRequestTicket(requestTicket);
            var history = new RequestTicketHistory();
            history.Content = $"Request canceled request ticket";
            history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
            history.RequestTicketId = requestTicket.RequestTicketId;
            history.LastUpdate = DateTime.Now;
            history.UserId = requestTicket.RequesterId;
            await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
        }

        public async Task UpdateTicketStateJobAsync()
        {
            // Logic to update ticket states
            // This could include your HandleStateForRequestTicket method

            // Example:
            var requestTickets = _requestTicketRepository.GetRequestTickets();
            foreach (var ticket in requestTickets)
            {
                await HandleStateForRequestTicket(ticket);
            }
        }

        private async Task HandleStateForRequestTicket(RequestTicket requestTicket)
        {
            if (requestTicket.State == StateEnum.Cancel.ToString())
            {
                return;
            }
            var responseTime = CalculateDatetime(requestTicket, true);
            var resolutionTime = CalculateDatetime(requestTicket, false);
            if ((requestTicket.LastUpdateAt <= resolutionTime && requestTicket.LastUpdateAt != null)
                || (resolutionTime >= requestTicket.ResolvedTime && requestTicket.ResolvedTime != null))
            {
                requestTicket.State = StateEnum.Normal.ToString();
            }
            if ((requestTicket.LastUpdateAt != null && requestTicket.LastUpdateAt > responseTime)
                 || (requestTicket.LastUpdateAt == null && DateTime.Now > responseTime))
            {
                requestTicket.State = StateEnum.OverdueResponseTime.ToString();
            }
            if (requestTicket.ResolvedTime != null && requestTicket.ResolvedTime > responseTime
                || (requestTicket.ResolvedTime == null && DateTime.Now > resolutionTime))
            {
                requestTicket.State = StateEnum.OverdueResolutionTime.ToString();
            }
            await _requestTicketRepository.UpdateRequestTicket(requestTicket);
        }

        private DateTime CalculateDatetime(RequestTicket requestTicket, bool isResponseDue)
        {
            Slametric slametric = requestTicket.Sla.Slametrics.Where(s => requestTicket.Priority == s.Priority).FirstOrDefault();
            return isResponseDue ? requestTicket.CreatedAt + TimeSpan.FromTicks(slametric.ResponseTime)
                : requestTicket.CreatedAt + TimeSpan.FromTicks(slametric.ResolutionTime);
        }

        public async Task<List<TicketQueryAdminDTO>> GetRequestTicketsQueryAdmin(QueryDTO queryDto)
        {
            var typeTicket = queryDto.QueryType ?? "all";
            var listTicket = new List<TicketQueryAdminDTO>();
            switch (typeTicket)
            {
                case "all":
                    listTicket = await _requestTicketRepository.GetRequestTicketsQueryAdmin(queryDto);
                    break;
                case "incident":
                    listTicket = await _requestTicketRepository.GetRequestTicketsQueryAdmin(queryDto);
                    break;
                case "problem":
                    listTicket = await _problemService.GetRequestTicketsQueryAdmin(queryDto);
                    break;
                case "change":
                    listTicket = await _changeService.GetRequestTicketsQueryAdmin(queryDto);
                    break;
                default:
                    break;
            }


            return listTicket;
        }

        public async Task<List<TicketQueryAdminDTO>> GetRequestTicketsAdmin(string ticketType, string queryId)
        {
            var typeTicket = ticketType;
            var listTicket = new List<TicketQueryAdminDTO>();
            var queryDto = new QueryDTO();
            var query = await _queryRepository.GetQueryById(queryId);
            if (query != null)
            {
                queryDto.QueryStatement = query.QueryStatement;
                queryDto.QueryType = ticketType;
            }
            switch (typeTicket)
            {
                case "all":
                    listTicket = await _requestTicketRepository.GetRequestTicketsQueryAdmin(queryDto);
                    break;
                case "incident":
                    listTicket = await _requestTicketRepository.GetRequestTicketsQueryAdmin(queryDto);
                    break;
                case "problem":
                    listTicket = await _problemService.GetRequestTicketsQueryAdmin(queryDto);
                    break;
                case "change":
                    listTicket = await _changeService.GetRequestTicketsQueryAdmin(queryDto);
                    break;
                default:
                    break;
            }
            //var listTicket = await _requestTicketRepository.GetRequestTicketsQueryAdmin(queryDto);
            return listTicket;
        }

        public async Task<List<TicketQueryAdminDTO>> GetRequestTicketsFilterUser(QueryConfigDTO queryDto)
        {
            var listTicket = await _requestTicketRepository.GetRequestTicketsFilterUser(queryDto);
            return listTicket;
        }
    }
}
