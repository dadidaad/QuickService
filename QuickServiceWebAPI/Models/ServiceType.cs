namespace QuickServiceWebAPI.Models;

public partial class ServiceType
{
    public string ServiceTypeId { get; set; } = null!;

    public string ServiceTypeName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
