using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.ServiceItem
{
    public class CreateUpdateServiceItemDTO
    {
        [Required]
        [MaxLength(100)]
        public string ServiceItemName { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string ShortDescription { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public int EstimatedDelivery { get; set; }

        public bool Status { get; set; }

        [Required]
        public string ServiceCategoryId { get; set; } = null!;

        [MaxLength(100)]
        public string? IconDisplay { get; set; }
        public string? WorkflowId { get; set; }
    }
}
