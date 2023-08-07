using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class Sla
{
    public string Slaid { get; set; } = null!;

    public string Slaname { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public bool IsDefault { get; set; }

    public string? ServiceItemId { get; set; }

    public bool? ForIncident { get; set; }

    public virtual ICollection<RequestTicket> RequestTickets { get; set; } = new List<RequestTicket>();

    public virtual ServiceItem? ServiceItem { get; set; }

    public virtual ICollection<Slametric> Slametrics { get; set; } = new List<Slametric>();

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();
}
