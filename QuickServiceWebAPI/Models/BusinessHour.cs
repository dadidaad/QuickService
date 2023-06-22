using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class BusinessHour
{
    public string BusinessHourId { get; set; } = null!;

    public string BusinessHourName { get; set; } = null!;

    public string TimeZone { get; set; } = null!;

    public bool IsDefault { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<ServiceDeskHour> ServiceDeskHours { get; set; } = new List<ServiceDeskHour>();

    public virtual ICollection<Slametric> Slametrics { get; set; } = new List<Slametric>();

    public virtual ICollection<YearlyHolidayList> YearlyHolidayLists { get; set; } = new List<YearlyHolidayList>();
}
