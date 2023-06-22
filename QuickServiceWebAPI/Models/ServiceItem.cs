using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class ServiceItem
{
    public string ServiceItemId { get; set; } = null!;

    public string ServiceItemName { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public string? Description { get; set; }

    public int EstimatedDelivery { get; set; }

    public string Status { get; set; } = null!;

    public string ServiceCategoryId { get; set; } = null!;

    public string? AttachmentId { get; set; }

    public virtual Attachment? Attachment { get; set; }

    public virtual ICollection<RequestTicket> RequestTickets { get; set; } = new List<RequestTicket>();

    public virtual ServiceCategory ServiceCategory { get; set; } = null!;
}
