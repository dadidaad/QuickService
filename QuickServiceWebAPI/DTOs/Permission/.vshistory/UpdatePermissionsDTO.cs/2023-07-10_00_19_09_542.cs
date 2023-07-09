using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Permission
{
    public class UpdatePermissionsDTO
    {
        [Required]
        [MaxLength(10)]
        public string RoleId { get; set; }
    }
}
