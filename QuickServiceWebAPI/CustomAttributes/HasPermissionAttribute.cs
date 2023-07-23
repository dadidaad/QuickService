using Microsoft.AspNetCore.Authorization;
using QuickServiceWebAPI.Utilities;
using QuickServiceWebAPI.Models.Enums;

namespace QuickServiceWebAPI.CustomAttributes
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(PermissionEnum permission, RoleType roleType)
            : base(policy: permission.GetDisplayName() + "," + roleType.ToString())
        {
        }
    }
}
