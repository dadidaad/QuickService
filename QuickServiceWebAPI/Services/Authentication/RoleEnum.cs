using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.Services.Authentication
{
    public enum RoleEnum
    {
        [Display(Name ="Super Admin")]
        SuperAdmin = 1,
    }
}
