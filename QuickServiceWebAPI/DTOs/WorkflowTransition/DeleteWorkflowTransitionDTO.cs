using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.WorkflowTransition
{
    public class DeleteWorkflowTransitionDTO
    {
        [Required]
        [MaxLength(10)]
        public string FromWorkflowTask { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string ToWorkflowTask { get; set; } = null!;
    }
}
