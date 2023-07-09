namespace QuickServiceWebAPI.DTOs.ServiceDeskHour
{
    public class CreateUpdateServiceDeskHourDTO
    {
        public string DayOfWeek { get; set; } = null!;

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public bool IsEnabled { get; set; }

        public string BusinessHourId { get; set; } = null!;
    }
}
