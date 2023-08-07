using QuickServiceWebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.WorkflowStep
{
    public class CreateUpdateWorkflowStepDTO
    {
        [Required]
        [MaxLength(255)]
        public string WorkflowStepName { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        [EnumDataType(typeof(StatusWorkflowStepEnum))]
        public string Status { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string ActionDetail { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string WorkflowId { get; set; } = null!;

        [MaxLength(10)]
        public string? AssignerId { get; set; }

        [MaxLength(10)]
        public string? GroupId { get; set; }
    }
}
