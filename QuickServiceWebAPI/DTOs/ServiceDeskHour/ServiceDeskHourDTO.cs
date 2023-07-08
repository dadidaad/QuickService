using QuickServiceWebAPI.DTOs.BusinessHour;

namespace QuickServiceWebAPI.DTOs.ServiceDeskHour
{
    public class ServiceDeskHourDTO
    {
        public string ServiceDeskHourId { get; set; } = null!;

        public string DayOfWeek { get; set; } = null!;

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public bool IsEnabled { get; set; }

        public string BusinessHourId { get; set; } = null!;

        public virtual BusinessHourDTO BusinessHourEntity { get; set; } = null!;
    }
}
