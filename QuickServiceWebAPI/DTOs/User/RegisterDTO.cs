using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.User
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Password { get; set; }

        [MaxLength(20)]
        public string? FirstName { get; set; }

        [MaxLength(20)]
        public string? MiddleName { get; set; }

        [MaxLength(20)]
        public string? LastName { get; set; }

        public bool IsActive { get; set; }
    }
}
