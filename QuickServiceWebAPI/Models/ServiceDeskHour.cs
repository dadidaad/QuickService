namespace QuickServiceWebAPI.Models;

public partial class ServiceDeskHour
{
    public string ServiceDeskHourId { get; set; } = null!;

    public string DayOfWeek { get; set; } = null!;

    public DateTime TimeStart { get; set; }

    public DateTime TimeEnd { get; set; }

    public bool IsEnabled { get; set; }

    public string BusinessHourId { get; set; } = null!;

    public virtual BusinessHour BusinessHour { get; set; } = null!;
}
