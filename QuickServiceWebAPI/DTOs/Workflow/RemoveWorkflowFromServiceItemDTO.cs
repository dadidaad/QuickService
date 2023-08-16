using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Workflow
{
    public class RemoveWorkflowFromServiceItemDTO
    {
        [Required]
        [MaxLength(10)]
        public string WorkflowId { get; set; } = null!;

        [ListHasElements]
        public List<string> ServiceItemIdList { get; set; } = new List<string>();
    }
}
