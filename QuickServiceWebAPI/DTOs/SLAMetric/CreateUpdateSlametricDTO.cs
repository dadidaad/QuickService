using QuickServiceWebAPI.DTOs.BusinessHour;
using QuickServiceWebAPI.DTOs.Sla;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.SLAMetric
{
    public class CreateUpdateSlametricDTO
    {
        [Required]
        [MaxLength(10)]
        public string Piority { get; set; } = null!;

        [Required]
        public DateTime ResponseTime { get; set; }

        [Required]
        public DateTime ResolutionTime { get; set; }

        [MaxLength(255)]
        public string? EscalationPolicy { get; set; }

        [MaxLength(255)]
        public string? NotificationRules { get; set; }

        [Required]
        public string BusinessHourId { get; set; } = null!;

        [Required]
        public string Slaid { get; set; } = null!;
    }
}
