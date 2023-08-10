using QuickServiceWebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.WorkflowTask
{
    public class CreateUpdateWorkflowTaskDTO
    {
        [Required]
        [MaxLength(255)]
        public string WorkflowTaskName { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        [EnumDataType(typeof(StatusWorkflowTaskEnum))]
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
