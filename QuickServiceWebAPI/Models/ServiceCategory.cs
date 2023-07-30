using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuickServiceWebAPI.Models;

public partial class ServiceCategory
{
    public string ServiceCategoryId { get; set; } = null!;

    public string ServiceCategoryName { get; set; } = null!;

    public string? Description { get; set; }

    [JsonIgnore]
    public virtual ICollection<ServiceItem> ServiceItems { get; set; } = new List<ServiceItem>();
}
