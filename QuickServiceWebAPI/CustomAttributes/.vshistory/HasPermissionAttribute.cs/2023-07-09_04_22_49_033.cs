using QuickServiceWebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace QuickServiceWebAPI.CustomAttributes
{
    public sealed class HasPermissionAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission)
            : base(policy: permission.PermissionName)
        {
        }
    }
}
