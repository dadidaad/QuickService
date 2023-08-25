﻿namespace QuickServiceWebAPI.Models;

public partial class Sla
{
    public string Slaid { get; set; } = null!;

    public string Slaname { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDefault { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<RequestTicket> RequestTickets { get; set; } = new List<RequestTicket>();

    public virtual ICollection<ServiceItem> ServiceItems { get; set; } = new List<ServiceItem>();

    public virtual ICollection<Slametric> Slametrics { get; set; } = new List<Slametric>();
}
