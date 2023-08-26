namespace QuickServiceWebAPI.Models;

public partial class ServiceItemCustomField
{
    public string ServiceItemId { get; set; } = null!;

    public string CustomFieldId { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public bool? Mandatory { get; set; }

    public virtual CustomField CustomField { get; set; } = null!;

    public virtual ServiceItem ServiceItem { get; set; } = null!;
}
