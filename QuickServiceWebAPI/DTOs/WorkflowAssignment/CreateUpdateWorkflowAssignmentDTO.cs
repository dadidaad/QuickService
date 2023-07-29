using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.WorkflowAssignment
{
    public class CreateUpdateWorkflowAssignmentDTO
    {
        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        [MaxLength(10)]
        public string RequestTicketId { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string WorkflowId { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string CurrentStepId { get; set; } = null!;
    }
}
