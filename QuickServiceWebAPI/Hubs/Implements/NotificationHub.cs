
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using QuickServiceWebAPI.DTOs.Notification;
using QuickServiceWebAPI.Helpers;

namespace QuickServiceWebAPI.Hubs.Implements
{
    [Authorize]
    public class NotificationHub : Hub<INotificationHub>
    {
        public async Task SendToGroup(string groupId, NotificationDTO notificationDTO)
        {
            await Clients.Group(groupId).SendMessage(notificationDTO);
        }
        public async Task AddUserToGroup(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }
        public async Task RemoveUserFromGroup(string groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        }


        public override async Task OnConnectedAsync()
        {
            var listGroup = GetGroups(Context); // get the username of the connected user

            foreach(var group in listGroup)
            {
                await AddUserToGroup(group);
            }
            
            await base.OnConnectedAsync();
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
