using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.WorkflowAssignment
{
    public class CheckWorkflowAssignmentDTO
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(10)]
        public string ReferenceId { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(10)]
        public string WorkflowId { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(10)]
        public string CurrentStepId { get; set; } = null!;

        public bool IsCompleted { get; set; }

        [MaxLength]
        public string? CompleteMessage { get; set; }

        [FileSize(1024 * 1024 * 10)]
        public IFormFile? File { get; set; }

    }
}
