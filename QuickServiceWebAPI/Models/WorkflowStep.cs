using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class WorkflowStep
{
    public string WorkflowStepId { get; set; } = null!;

    public string WorkflowStepName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string ActionDetail { get; set; } = null!;

    public string WorkflowId { get; set; } = null!;

    public string? AssignerId { get; set; }

    public string? GroupId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Workflow Workflow { get; set; } = null!;

    public virtual ICollection<WorkflowAssignment> WorkflowAssignments { get; set; } = new List<WorkflowAssignment>();
}
