using Microsoft.AspNetCore.Http;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Middlewares
{
    public class AuthenticateHubMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticateHubMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            var request = context.Request;

            // web sockets cannot pass headers so we must take the access token from query param and
            // add it to the header before authentication middleware  runs
            if (request.Path.StartsWithSegments("/hub", StringComparison.OrdinalIgnoreCase) &&
                request.Query.TryGetValue("access_token", out var accessToken))
            {
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }
            await _next(context);
        }
    }
}
