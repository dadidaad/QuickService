using Microsoft.AspNetCore.Authorization;
using QuickServiceWebAPI.Helpers;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;
using System.Security.Claims;

namespace QuickServiceWebAPI.Services.Authentication
{
    public class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var permissions = context
                .User
                .Claims
                .Where(x => x.Type == CustomClaims.Permissions)
                .Select(x => x.Value)
                .ToHashSet();
            var roleType = context
                .User
                .Claims
                .Where(x => x.Type == CustomClaims.RoleType)
                .SingleOrDefault()?.Value;
            var role = context
                .User
                .Claims
                .Where(x => x.Type == ClaimTypes.Role)
                .SingleOrDefault()?.Value;

            bool checkPermission = (permissions.Contains(requirement.Permission)
                && roleType == requirement.RoleType)
                || (role == RoleEnum.SuperAdmin.GetDisplayName());
            if (checkPermission)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}
