using Microsoft.AspNetCore.Authorization;

namespace QuickServiceWebAPI.Services.Authentication
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permission, string roleType)
        {
            Permission = permission;
        }

        public string Permission { get; }
        public string RoleType { get; }
    }
}
