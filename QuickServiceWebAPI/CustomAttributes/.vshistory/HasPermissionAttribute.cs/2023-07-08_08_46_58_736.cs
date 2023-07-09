using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.CustomAttributes
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission)
            : base(policy: permission.ToString())
        {
        }
    }
}
