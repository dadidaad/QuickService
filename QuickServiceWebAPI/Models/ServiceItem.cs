using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class ServiceItem
{
    public string ServiceItemId { get; set; } = null!;

    public string ServiceItemName { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public string? Description { get; set; }

    public int? EstimatedDelivery { get; set; }

    public string Status { get; set; } = null!;

    public string ServiceCategoryId { get; set; } = null!;

    public string? IconDisplay { get; set; }

    public virtual ICollection<RequestTicket> RequestTickets { get; set; } = new List<RequestTicket>();

    public virtual ServiceCategory ServiceCategory { get; set; } = null!;

    public virtual ICollection<ServiceItemCustomField> ServiceItemCustomFields { get; set; } = new List<ServiceItemCustomField>();

    public virtual ICollection<Sla> Slas { get; set; } = new List<Sla>();

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();
}
