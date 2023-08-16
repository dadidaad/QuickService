using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.SLAMetric
{
    public class CreateSlametricDTO
    {

        [Required]
        [MaxLength(10)]
        [EnumDataType(typeof(PriorityEnum))]
        public string Piority { get; set; } = null!;

        [Required]
        public long ResponseTime { get; set; }

        [Required]
        public long ResolutionTime { get; set; }

        [MaxLength(255)]
        public string? EscalationPolicy { get; set; }


        [MaxLength(255)]
        public string? NotificationRules { get; set; }

        public string SlaId { get; set; } = null!;

    }
}
