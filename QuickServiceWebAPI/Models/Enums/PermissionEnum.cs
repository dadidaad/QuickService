using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.Models.Enums
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
        ManageCustomFields,

        [Display(Name = "Manage groups")]
        ManageGroups,

        [Display(Name = "Manage slas")]
        ManageSlas,

        [Display(Name = "Manage workflows")]
        ManageWorkflows,

        [Display(Name = "Manage dashboard")]
        ManageDashboard,

        [Display(Name = "Manage changes")]
        ManageChange,

        [Display(Name = "Manage problems")]
        ManageProblems
    }
}
