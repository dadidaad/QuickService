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
        public string CurrentTaskId { get; set; } = null!;

        public bool IsCompleted { get; set; }

        [Required]
        [MaxLength(10)]
        public string ReceiverId { get; set; } = null!;

        [MaxLength]
        public string? CompleteMessage { get; set; }

        [FileSize(1024 * 1024 * 10)]
        public IFormFile? File { get; set; }

    }
}
