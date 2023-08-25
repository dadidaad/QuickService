using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.JsonWebTokens;
using QuickServiceWebAPI.Helpers;
using System.Security.Claims;

namespace QuickServiceWebAPI.Hubs
{
    public class UserCustomProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            var httpContext  = connection.GetHttpContext();

            var userId = connection.User.Claims
                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)
                .Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return string.Empty;
            }

            return $"{userId}";
        }
    }
}
