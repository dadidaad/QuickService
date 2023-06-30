namespace QuickServiceWebAPI.DTOs.Workflow
{
    public class CreateUpdateWorkflowDTO
    {
        public string WorkflowName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Description { get; set; }

        public string CreatedBy { get; set; } = null!;
    }
}
