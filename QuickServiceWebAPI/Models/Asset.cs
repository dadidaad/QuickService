using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class Asset
{
    public string AssetId { get; set; } = null!;

    public string AssetName { get; set; } = null!;

    public string AssetType { get; set; } = null!;

    public string? Description { get; set; }

    public string? Manufacturer { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? Location { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<AssetAssignment> AssetAssignments { get; set; } = new List<AssetAssignment>();

    public virtual ICollection<AssetHistory> AssetHistories { get; set; } = new List<AssetHistory>();
}
