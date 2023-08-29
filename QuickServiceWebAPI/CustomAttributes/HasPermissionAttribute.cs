using Microsoft.AspNetCore.Authorization;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Utilities;
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
