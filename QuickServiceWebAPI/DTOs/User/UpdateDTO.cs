using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.User
{
    public class UpdateDTO
    {
        [Required]
        [MaxLength(10)]
        public string? UserId { get; set; }

        [MaxLength(20)]
        public string? FirstName { get; set; }

        [MaxLength(20)]
        public string? MiddleName { get; set; }

        [MaxLength(20)]
        public string? LastName { get; set; }

        [RegularExpression("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$"
            , ErrorMessage = "Invalid phone number format")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Avatar")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile? AvatarUpload { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
            , ErrorMessage = "Invalid Email format")]
        public string? PersonalEmail { get; set; }

        public DateTime? BirthDate { get; set; }
        public string? JobTitle { get; set; }
        public string? Department { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
