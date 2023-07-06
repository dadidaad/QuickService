using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.BusinessHour
{
    public class CreateUpdateBusinessHourDTO
    {
        [Required]
        [MaxLength(100)]
        public string BusinessHourName { get; set; }

        [Required]
        [MaxLength(2)]
        public string TimeZone { get; set; } = null!;

        public bool IsDefault { get; set; }
    }
}
