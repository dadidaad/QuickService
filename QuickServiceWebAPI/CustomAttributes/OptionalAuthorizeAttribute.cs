using Microsoft.AspNetCore.Authorization;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.CustomAttributes
{
    public class OptionalAuthorizeAttribute : AuthorizeAttribute
    {
        public OptionalAuthorizeAttribute(PermissionEnum permission)
            : base(policy: permission.GetDisplayName())
        {
        }
    }
}
