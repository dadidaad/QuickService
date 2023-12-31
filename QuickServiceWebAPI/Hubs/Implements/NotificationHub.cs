﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using QuickServiceWebAPI.DTOs.Notification;
using QuickServiceWebAPI.Helpers;

namespace QuickServiceWebAPI.Hubs.Implements
{
    [Authorize]
    public class NotificationHub : Hub<INotificationHub>
    {

        private readonly ILogger<NotificationHub> _logger;

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
        }

        public async Task SendMessage(NotificationDTO notificationDTO)
        {
            await Clients.All.ReceiveNotification(notificationDTO);
        }

        public async Task SendText(string text)
        {
            await Clients.All.ReceiveNormalMessage(text);
        }

        public async Task SendToGroup(string groupId, NotificationDTO notificationDTO)
        {
            await Clients.Group(groupId).ReceiveNotification(notificationDTO);
        }

        public async Task SendToUser(string userId, NotificationDTO notificationDTO)
        {
            await Clients.Users(userId).ReceiveNotification(notificationDTO);
        }
        public async Task AddUserToGroup(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }
        public async Task RemoveUserFromGroup(string groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        }


        public override async Task<string> OnConnectedAsync()
        {
            var listGroup = GetGroups(Context); // get the username of the connected user

            foreach (var group in listGroup)
            {
                await AddUserToGroup(group);
            }

            _logger.LogTrace("Connected");
            await base.OnConnectedAsync();
            return Context.ConnectionId;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {


            return base.OnDisconnectedAsync(exception);

        }

        private List<string> GetGroups(HubCallerContext context)
        {
            var groups = new List<string>();
            var httpContext = context.GetHttpContext();
            groups = httpContext.User.Claims.Where(c => c.Type == CustomClaims.Groups).Select(c => c.Value).ToList();
            return groups;
        }
    }
}
