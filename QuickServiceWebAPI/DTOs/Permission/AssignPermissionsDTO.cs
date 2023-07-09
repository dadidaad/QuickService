using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Permission
{
    public class AssignPermissionsDTO
    {
        [Required]
        [MaxLength(10)]
        public string? RoleId { get; set; }

        [ListHasElements (ErrorMessage = "Must select at least one permission")]
        public List<string>? PermissionIdList { get; set; }

    }
}
