using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class Workflow
{
    public string WorkflowId { get; set; } = null!;

    public string WorkflowName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? ReferenceId { get; set; }

    public bool ForIncident { get; set; }

    public string? Slaid { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ServiceItem? Reference { get; set; }

    public virtual Sla? Sla { get; set; }

    public virtual ICollection<WorkflowAssignment> WorkflowAssignments { get; set; } = new List<WorkflowAssignment>();

    public virtual ICollection<WorkflowStep> WorkflowSteps { get; set; } = new List<WorkflowStep>();
}
