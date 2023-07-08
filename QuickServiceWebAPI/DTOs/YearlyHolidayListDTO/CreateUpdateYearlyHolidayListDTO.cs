﻿namespace QuickServiceWebAPI.DTOs.YearHolidayListDTO
{
    public class CreateUpdateYearlyHolidayListDTO
    {
        public DateTime Holiday { get; set; }

        public string HolidayName { get; set; } = null!;

        public string BusinessHourId { get; set; } = null!;
    }
}
