using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.WorkflowAssignment
{
    public class RejectWorkflowTaskDTO
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(10)]
        public string ReferenceId { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(10)]
        public string CurrentStepId { get; set; } = null!;

        public bool IsReject { get; set; }

        [MaxLength(255)]
        [Required(AllowEmptyStrings = false)]
        public string RejectReason { get; set; } = null!;
    }
}
