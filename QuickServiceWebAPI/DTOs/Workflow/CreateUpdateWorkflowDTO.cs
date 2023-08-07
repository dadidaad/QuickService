using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Workflow
{
    public class CreateUpdateWorkflowDTO
    {
        [Required]
        [MaxLength(255)]
        public string WorkflowName { get; set; }


        [Required]
        [MaxLength(10)]
        public string CreatedBy { get; set; }
    }
}
