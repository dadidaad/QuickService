﻿using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Middlewares
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJWTUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userClaims = jwtUtils.ValidateToken(token);
            if (userClaims != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = userClaims;
            }

            await _next(context);
        }
    }
}
