using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class Service
{
    public string ServiceId { get; set; } = null!;

    public string ServiceName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Impact { get; set; } = null!;

    public string HealthStatus { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string ServiceTypeId { get; set; } = null!;

    public string ManagedBy { get; set; } = null!;

    public string? ManagedByGroup { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Group? ManagedByGroupNavigation { get; set; }

    public virtual User ManagedByNavigation { get; set; } = null!;

    public virtual ServiceType ServiceType { get; set; } = null!;
}
