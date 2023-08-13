using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.DTOs.User;

namespace QuickServiceWebAPI.DTOs.Workflow
{
    public class WorkflowDTO
    {
        public string WorkflowId { get; set; } = null!;

        public string WorkflowName { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string? Description { get; set; }

        public virtual UserDTO User { get; set; } = null!;

        public virtual List<ServiceItemDTOSecond> ServiceItemDTOs { get; set; } = new List<ServiceItemDTOSecond>();
    }
}
