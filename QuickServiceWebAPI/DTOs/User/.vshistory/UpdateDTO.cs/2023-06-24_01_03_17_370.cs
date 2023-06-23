using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.User
{
    public class UpdateDTO
    {
        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [MaxLength(100)]
        public string? Password { get; set; }

        [MaxLength(20)]
        public string? FirstName { get; set; }

        [MaxLength(20)]
        public string? MiddleName { get; set; }

        [MaxLength(20)]
        public string? LastName { get; set; }

        [MaxLength(10)]
        [RegularExpression("/\\(?([0-9]{3})\\)?([ .-]?)([0-9]{3})\\2([0-9]{4})/", ErrorMessage = "Invalid phone number format")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Avatar")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile? Avatar { get; set; }

        public bool IsActive { get; set; }
    }
}
