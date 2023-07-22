using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.Services.Authentication
{
    public enum PermissionEnum
    {
        [Display(Name = "Manage users")]
        ManageUsers,

        [Display(Name = "Manage roles")]
        ManageRoles,

        [Display(Name = "Manage tickets")]
        ManageTickets,

        [Display(Name = "Manage service items")]
        ManageServiceItems,

        [Display(Name = "Manage service categories")]
        ManageServiceCategories,

        [Display(Name = "Manage custom fields")]
        ManageCustomFields
    }
}
