using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Permission
{
    public class CreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string? PermissionName { get; set; }
    }
}
