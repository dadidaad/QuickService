using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.WorkflowAssignment
{
    public class AssignTaskToAgentDTO
    {
        [MaxLength(10)]
        [Required]
        public string WorkflowAssignmentId { get; set; } = null!;

        [MaxLength(10)]
        [Required]
        public string AssigneeId { get; set; } = null!;
    }
}
