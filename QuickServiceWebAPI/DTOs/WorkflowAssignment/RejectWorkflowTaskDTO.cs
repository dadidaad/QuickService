using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.WorkflowAssignment
{
    public class RejectWorkflowTaskDTO
    {
        [MaxLength(10)]
        [Required]
        public string WorkflowAssignmentId { get; set; } = null!;

        public bool IsReject { get; set; }

        [MaxLength(255)]
        [Required(AllowEmptyStrings = false)]
        public string RejectReason { get; set; } = null!;
    }
}
