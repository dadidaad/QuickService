using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.User
{
    public class AssignRoleDTO
    {
        [Required]
        [MaxLength(10)]
        public string? UserId { get; set; }
        [Required]
        [MaxLength(10)]
        public string? RoleId { get; set; }
        
    }
}
