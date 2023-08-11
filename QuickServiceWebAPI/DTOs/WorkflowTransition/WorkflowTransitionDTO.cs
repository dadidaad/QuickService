using QuickServiceWebAPI.DTOs.WorkflowTask;

namespace QuickServiceWebAPI.DTOs.WorkflowTransition
{
    public class WorkflowTransitionDTO
    {
        public string FromWorkflowTask { get; set; } = null!;

        public string ToWorkflowTask { get; set; } = null!;

        public string WorkflowTransitionName { get; set; } = null!;

        public bool Condition { get; set; }

        public virtual WorkflowTaskDTO FromWorkflowTaskNavigation { get; set; } = null!;

        public virtual WorkflowTaskDTO ToWorkflowTaskNavigation { get; set; } = null!;
    }
}
