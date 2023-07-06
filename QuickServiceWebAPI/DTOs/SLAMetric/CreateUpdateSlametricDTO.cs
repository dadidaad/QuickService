using QuickServiceWebAPI.DTOs.BusinessHour;
using QuickServiceWebAPI.DTOs.Sla;

namespace QuickServiceWebAPI.DTOs.SLAMetric
{
    public class CreateUpdateSlametricDTO
    {
        public string Piority { get; set; } = null!;

        public DateTime ResponseTime { get; set; }

        public DateTime ResolutionTime { get; set; }

        public string? EscalationPolicy { get; set; }

        public string? NotificationRules { get; set; }

        public string BusinessHourId { get; set; } = null!;

        public string Slaid { get; set; } = null!;
    }
}
