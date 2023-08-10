using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.WorkflowTransition
{
    public class WorkflowTransitionDTO
    {
        [Required]
        [MaxLength(10)]
        public string FromWorkflowTask { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string ToWorkflowTask { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string WorkflowTransitionName { get; set; } = null!;

        public bool Condition { get; set; }
    }
}
