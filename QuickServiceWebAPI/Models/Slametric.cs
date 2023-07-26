﻿using Newtonsoft.Json;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class Slametric
{
    public string SlametricId { get; set; } = null!;

    public string Piority { get; set; } = null!;

    //[JsonConverter(typeof(IntToTimeSpanConverter))]
    public TimeSpan ResponseTime { get; set; }
    
    //[JsonConverter(typeof(IntToTimeSpanConverter))]
    public TimeSpan ResolutionTime { get; set; }

    public string? EscalationPolicy { get; set; }

    public string? NotificationRules { get; set; }

    public string? BusinessHourId { get; set; }

    public string Slaid { get; set; } = null!;

    public virtual BusinessHour? BusinessHour { get; set; }

    public virtual Sla Sla { get; set; } = null!;
}
