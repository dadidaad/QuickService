using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Role
{
    public class RoleDTO
    {
        [Required]
        [MaxLength(50)]
        public string? RoleName { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }

        
    }
}
