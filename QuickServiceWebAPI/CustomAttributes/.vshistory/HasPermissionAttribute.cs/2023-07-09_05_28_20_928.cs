using QuickServiceWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using QuickServiceWebAPI.Services.Authentication;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.CustomAttributes
{
    public sealed class HasPermissionAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public HasPermissionAttribute(PermissionEnum permission, RoleType roleType)
            : base(policy: permission.GetDisplayName() + "," + roleType.ToString())
        {
        }
    }
}
