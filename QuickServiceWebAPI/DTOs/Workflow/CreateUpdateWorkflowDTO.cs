using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Workflow
{
    public class CreateUpdateWorkflowDTO
    {
        [Required]
        [MaxLength(255)]
        public string WorkflowName { get; set; } = null!;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string CreatedBy { get; set; } = null!;
    }
}
