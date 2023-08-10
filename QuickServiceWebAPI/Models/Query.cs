using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class Query
{
    public string QueryId { get; set; } = null!;

    public string? QueryName { get; set; }

    public string? QueryStatement { get; set; }

    public bool? IsTeamQuery { get; set; }

    public string? UserId { get; set; }

    public virtual User? User { get; set; }
}
