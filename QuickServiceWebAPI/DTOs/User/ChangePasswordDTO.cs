using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.User
{
    public class ChangePasswordDTO
    {

        [Required]
        [MaxLength(10)]
        public string? UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? OldPassword { get; set; }

        [Required]
        [MaxLength(100)]
        public string? NewPassword { get; set; }

        [MaxLength(100)]
        [Compare("NewPassword", ErrorMessage = "Must match new password")]
        public string? ConfirmPassword { get; set; }
    }
}
