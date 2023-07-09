using QuickServiceWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using QuickServiceWebAPI.Services.Authentication;

namespace QuickServiceWebAPI.CustomAttributes
{
    public sealed class HasPermissionAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public HasPermissionAttribute(PermissionEnum permission)
            : base(policy: permission.ToString())
        {
        }
    }
}
