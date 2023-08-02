using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.SLAMetric
{
    public class CreateUpdateSlametricDTO
    {
        [Required]
        [MaxLength(10)]
        public string Piority { get; set; } = null!;

        [Required]
        public long ResponseTime { get; set; }

        [Required]
        public long ResolutionTime { get; set; }

        [MaxLength(255)]
        public string? EscalationPolicy { get; set; }

        [MaxLength(255)]
        public string? NotificationRules { get; set; }

        public string? BusinessHourId { get; set; }

        [Required]
        public string Slaid { get; set; } = null!;
    }
}
