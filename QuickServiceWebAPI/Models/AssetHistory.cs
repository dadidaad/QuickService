using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class AssetHistory
{
    public string AssetHistoryId { get; set; } = null!;

    public string Action { get; set; } = null!;

    public DateTime ActionTime { get; set; }

    public string? Description { get; set; }

    public string AssetId { get; set; } = null!;

    public virtual Asset Asset { get; set; } = null!;
}
