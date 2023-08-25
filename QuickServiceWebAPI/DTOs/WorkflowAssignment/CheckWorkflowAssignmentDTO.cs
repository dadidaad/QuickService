using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.WorkflowAssignment
{
    public class CheckWorkflowAssignmentDTO
    {
        [MaxLength(10)]
        [Required]
        public string WorkflowAssignmentId { get; set; } = null!;

        public bool IsCompleted { get; set; }

        [Required]
        [MaxLength(10)]
        public string FinisherId { get; set; } = null!;

        [MaxLength]
        public string? Message { get; set; }

        [FileSize(1024 * 1024 * 10)]
        public IFormFile? File { get; set; }

        [MaxLength(10)]
        [Required]
        public string ToWorkFlowTask { get; set; } = null!;
    }
}
