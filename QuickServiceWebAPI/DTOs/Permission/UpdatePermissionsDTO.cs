using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Permission
{
    public class UpdatePermissionsDTO
    {
        [Required]
        [MaxLength(10)]
        public string? RoleId { get; set; }

        [ListHasElements]
        public List<PermissionDTO> Permissions { get; set; } = new List<PermissionDTO>();
    }
}
