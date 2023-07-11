using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.DTOs.ServiceItem
{
    public class ServiceItemDTO
    {
        public string ServiceItemId { get; set; } = null!;

        public string ServiceItemName { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public string? Description { get; set; }

        public int EstimatedDelivery { get; set; }

        public string Status { get; set; } = null!;

        public string ServiceCategoryId { get; set; } = null!;

        public string? AttachmentId { get; set; }

        public virtual AttachmentDTO? AttachmentEntity { get; set; }

        public virtual ServiceCategoryDTO ServiceCategoryEntity { get; set; } = null!;
    }
}
