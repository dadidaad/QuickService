using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class YearlyHolidayList
{
    public DateTime Holiday { get; set; }

    public string HolidayName { get; set; } = null!;

    public string BusinessHourId { get; set; } = null!;

    public virtual BusinessHour BusinessHour { get; set; } = null!;
}
