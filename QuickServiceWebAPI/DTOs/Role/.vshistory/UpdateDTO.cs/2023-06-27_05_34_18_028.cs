using QuickServiceWebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Role
{
    public class UpdateDTO
    {
        [Required]
        [MaxLength(50)]
        public string? RoleName { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }

        public RoleType RoleType { get; set; }
    }

}
