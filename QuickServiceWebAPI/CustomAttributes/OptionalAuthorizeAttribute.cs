using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
