using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.WorkflowStep
{
    public class CreateUpdateWorkflowStepDTO
    {
        [Required]
        [MaxLength(255)]
        public string WorkflowStepName { get; set; }

        [Required]
        [MaxLength(100)]
        public string ActionType { get; set; }

        [Required]
        [MaxLength(255)]
        public string ActionDetails { get; set; }

        [Required]
        [MaxLength(10)]
        public string WorkflowId { get; set; } 
    }
}
