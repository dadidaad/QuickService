namespace QuickServiceWebAPI.Models;

public partial class WorkflowTransition
{
    public string FromWorkflowTask { get; set; } = null!;

    public string ToWorkflowTask { get; set; } = null!;

    public string WorkflowTransitionName { get; set; } = null!;

    public bool Condition { get; set; }

    public virtual WorkflowTask FromWorkflowTaskNavigation { get; set; } = null!;

    public virtual WorkflowTask ToWorkflowTaskNavigation { get; set; } = null!;
}
