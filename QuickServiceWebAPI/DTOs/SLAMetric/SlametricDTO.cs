namespace QuickServiceWebAPI.DTOs.SLAMetric
{
    public class SlametricDTO
    {
        public string SlametricId { get; set; } = null!;

        public string Priority { get; set; } = null!;

        public long ResponseTime { get; set; }

        public long ResolutionTime { get; set; }

        public string? EscalationPolicy { get; set; }

        public string? NotificationRules { get; set; }

        public string BusinessHourId { get; set; } = null!;

    }
}
