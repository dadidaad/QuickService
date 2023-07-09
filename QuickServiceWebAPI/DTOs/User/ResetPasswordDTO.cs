using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.User
{
    public class ResetPasswordDTO
    {
        [Required]
        [MaxLength(10)]
        public string UserId { get; set; }
    }
}
