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

        [MaxLength(10)]
        public string? RoleId { get; set;}
        [MaxLength(20)]
        public string? LastName { get; set; }

        [RegularExpression("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$"
            , ErrorMessage = "Invalid phone number format")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Avatar")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile? Avatar { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
