namespace QuickServiceWebAPI.Models;

public partial class Workflow
{
    public string WorkflowId { get; set; } = null!;

    public string WorkflowName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<RequestTicket> RequestTickets { get; set; } = new List<RequestTicket>();

    public virtual ICollection<ServiceItem> ServiceItems { get; set; } = new List<ServiceItem>();

    public virtual ICollection<WorkflowTask> WorkflowTasks { get; set; } = new List<WorkflowTask>();
}
