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

        [Display(Name = "Manage attachments")]
        ManageAttachments,

        [Display(Name = "Manage groups")]
        ManageGroups,

        [Display(Name = "Manage service desk hours")]
        ManageServiceDeskHours,

        [Display(Name = "Manage services")]
        ManageServices,

        [Display(Name = "Manage service types")]
        ManageServiceTypes,

        [Display(Name = "Manage slametrics")]
        ManageSlametrics,

        [Display(Name = "Manage slas")]
        ManageSlas,

        [Display(Name = "Manage workflows")]
        ManageWorkflows,

        [Display(Name = "Manage workflow steps")]
        ManageWorkflowSteps,

        [Display(Name = "Manage yearly holiday list")]
        ManageYearlyHolidayList,
    }
}
