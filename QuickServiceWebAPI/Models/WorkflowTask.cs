using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class WorkflowTask
{
    public string WorkflowTaskId { get; set; } = null!;

    public string WorkflowTaskName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string WorkflowId { get; set; } = null!;

    public string? AssignerId { get; set; }

    public string? GroupId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Workflow Workflow { get; set; } = null!;

    public virtual ICollection<WorkflowAssignment> WorkflowAssignments { get; set; } = new List<WorkflowAssignment>();

    public virtual ICollection<WorkflowTransition> WorkflowTransitionFromWorkflowTaskNavigations { get; set; } = new List<WorkflowTransition>();

    public virtual ICollection<WorkflowTransition> WorkflowTransitionToWorkflowTaskNavigations { get; set; } = new List<WorkflowTransition>();
}
