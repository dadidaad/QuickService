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

        [MaxLength(10)]
        [Required]
        public string ServiceCategoryId { get; set; } = null!;

        [MaxLength(100)]
        public string? IconDisplay { get; set; }

        [MaxLength(10)]
        [Required]
        public string WorkflowId { get; set; } = null!;
        [MaxLength(10)]
        [Required]
        public string SlaId { get; set; } = null!;
    }
}
