using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.User
{
    public class AuthenticateRequestDTO
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
