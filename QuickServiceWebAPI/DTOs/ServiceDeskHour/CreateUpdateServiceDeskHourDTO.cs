using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.ServiceDeskHour
{
    public class CreateUpdateServiceDeskHourDTO
    {
        [Required]
        [MaxLength(10)]
        public string DayOfWeek { get; set; } = null!;

        [Required]
        public DateTime TimeStart { get; set; }

        [Required]
        public DateTime TimeEnd { get; set; }

        [Required]
        public bool IsEnabled { get; set; }

        [Required]
        public string BusinessHourId { get; set; } = null!;
    }
}
