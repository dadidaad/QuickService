using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using QuickServiceWebAPI.DTOs.Notification;
using QuickServiceWebAPI.Hubs;
using QuickServiceWebAPI.Hubs.Implements;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class NotificationService : INotificationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRequestTicketRepository _requestTicketRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly INotificationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationService> _logger;
        private readonly IHubContext<NotificationHub, INotificationHub> _notificationHub;

        public NotificationService(INotificationRepository repository,
            IMapper mapper,
            ILogger<NotificationService> logger,
            IUserRepository userRepository,
            IRequestTicketRepository requestTicketRepository,
            IGroupRepository groupRepository,
            IHubContext<NotificationHub, INotificationHub> notificationHub)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _userRepository = userRepository;
            _requestTicketRepository = requestTicketRepository;
            _groupRepository = groupRepository;
            _notificationHub = notificationHub;
        }


        public static string TargetUrlForRequestTicket = "admin/tickets/";
        public async Task AddNotifications(AddNotificationDTO addNotificationDTO)
        {
            try
            {
                var requestTicket = await _requestTicketRepository.GetRequestTicketById(addNotificationDTO.RelateId);
                Group? group = null;
                if (!string.IsNullOrEmpty(addNotificationDTO.ToGroupId))
                {
                    group = await _groupRepository.GetGroupById(addNotificationDTO.ToGroupId);
                }
                var notification = GetCaseOfNotification(addNotificationDTO.NotificationType, requestTicket, group, addNotificationDTO.ToUserId);
                notification.CreatedDate = DateTime.Now;
                notification.IsRead = false;
                notification.NotificationId = await GetNextId();
                bool isInserted = await _repository.AddNotification(notification);
                if (isInserted)
                {
                    await _notificationHub.Clients.All.SendNormalMessage("Ok");
                    await _notificationHub.Clients.All.SendMessageAsync(_mapper.Map<NotificationDTO>(notification));
                    if (!string.IsNullOrEmpty(notification.ToUserId))
                    {
                        await _notificationHub.Clients.User(notification.ToUserId).SendMessageAsync(_mapper.Map<NotificationDTO>(notification));
                    }
                    else if(!string.IsNullOrEmpty(notification.ToGroupId))
                    {
                        await _notificationHub.Clients.Group(notification.ToGroupId).SendMessageAsync(_mapper.Map<NotificationDTO>(notification));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Task DeleteNotification()
        {
            throw new NotImplementedException();
        }

        public async Task<List<NotificationDTO>> GetNotifications(string ToUserId, bool isGetOnlyUnRead)
        {
            try
            {
                var user = await _userRepository.GetUserDetails(ToUserId);
                if (user == null)
                {
                    throw new AppException($"User with id {ToUserId} not found");
                }
                var groupIdList = user.GroupsNavigation.Select(g => g.GroupId).ToList();
                var notifications = await _repository.GetNotifications(ToUserId, groupIdList, isGetOnlyUnRead);
                return _mapper.Map<List<NotificationDTO>>(notifications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        private async Task<string> GetNextId()
        {
            Notification lastNotification = await _repository.GetLastNotification();
            int id = 0;
            if (lastNotification == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastNotification.NotificationId) + 1;
            }
            string notificationId = IDGenerator.GenerateNotificationId(id);
            return notificationId;
        }

        public Task UpdateNotification()
        {
            throw new NotImplementedException();
        }


        private Notification GetCaseOfNotification(NotificationTypeEnum notificationType
            , RequestTicket requestTicket, Group? toGroup, string? toUserId)
        {
            var notification = new Notification();
            switch (notificationType)
            {
                case NotificationTypeEnum.AssignUser:
                    {
                        if(toUserId != null)
                        {
                            notification.NotificationHeader = "Ticket assigned";
                            notification.NotificationBody = $"You have" +
                                $" assigned {requestTicket.RequestTicketId}" +
                                $"\n Request ticket: {requestTicket.Title}";
                            notification.TargetUrl = $"{TargetUrlForRequestTicket}{requestTicket.RequestTicketId}";
                            notification.RelateId = requestTicket.RequestTicketId;
                            notification.ToUserId = toUserId;
                        }
                        break;
                    }
                case NotificationTypeEnum.AssignGroup:
                    {
                        if(toGroup != null)
                        {
                            notification.NotificationHeader = "Ticket assigned";
                            notification.NotificationBody = $"Request ticket with id " +
                                $"{requestTicket.RequestTicketId} has been assigned to your group {toGroup?.GroupName}" +
                                $"\n Request ticket: {requestTicket.Title}";
                            notification.TargetUrl = $"{TargetUrlForRequestTicket}{requestTicket.RequestTicketId}";
                            notification.RelateId = requestTicket.RequestTicketId;
                            notification.ToGroupId = toGroup?.GroupId;
                        }
                        break;
                    }
                case NotificationTypeEnum.Discuss:
                    {
                        notification.NotificationHeader = "Ticket discuss";
                        notification.NotificationBody = $"You have been metioned" +
                            $" in request ticket {requestTicket.RequestTicketId}" +
                            $"\n Request ticket: {requestTicket.Title}";
                        notification.TargetUrl = $"{TargetUrlForRequestTicket}{requestTicket.RequestTicketId}";
                        notification.RelateId = requestTicket.RequestTicketId;
                        break;
                    }
                case NotificationTypeEnum.Approval:
                    {
                        //notification.NotificationHeader = "Change approval";
                        //notification.NotificationBody = $"" +
                        //    $" need your approval in {requestTicket.RequestTicketId}" +
                        //    $"\n Request ticket: {requestTicket.Title}";
                        break;
                    }
                default: break;
            }
            return notification;
        }
    }
}
