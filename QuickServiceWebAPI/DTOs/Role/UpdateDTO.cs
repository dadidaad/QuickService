using QuickServiceWebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Role
{
    public class UpdateDTO
    {
        [Required]
        [MaxLength(10)]
        public string? RoleId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? RoleName { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        [Required]
        public RoleType RoleType { get; set; }
    }

}
