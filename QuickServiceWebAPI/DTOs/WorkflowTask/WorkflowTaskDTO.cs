using QuickServiceWebAPI.DTOs.Workflow;

namespace QuickServiceWebAPI.DTOs.WorkflowTask
{
    public class WorkflowTaskDTO
    {
        public string? WorkflowStepId { get; set; }

        public string? WorkflowStepName { get; set; }

        public string? ActionType { get; set; }

        public string? ActionDetails { get; set; }

        public string? WorkflowId { get; set; }

        public virtual WorkflowDTO WorkflowEntity { get; set; } = null!;
    }
}
