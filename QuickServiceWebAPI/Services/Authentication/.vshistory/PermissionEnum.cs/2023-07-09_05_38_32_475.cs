using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.Services.Authentication
{
    public enum PermissionEnum
    {
        [Display(Name = "Manage users")]
        ManageUsers,

        [Display(Name = "Manage roles")]
        ManageRoles,
    }
}
