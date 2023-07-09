using QuickServiceWebAPI.DTOs.BusinessHour;

namespace QuickServiceWebAPI.DTOs.YearHolidayList
{
    public class YearlyHolidayListDTO
    {
        public DateTime Holiday { get; set; }

        public string HolidayName { get; set; } = null!;

        public string BusinessHourId { get; set; } = null!;

        public virtual BusinessHourDTO BusinessHourEntity { get; set; } = null!;
    }
}
