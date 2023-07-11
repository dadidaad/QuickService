namespace QuickServiceWebAPI.DTOs.ServiceItem
{
    public class CreateUpdateServiceItemDTO
    {
        public string ServiceItemName { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public string? Description { get; set; }

        public int EstimatedDelivery { get; set; }

        public string Status { get; set; } = null!;

        public string ServiceCategoryId { get; set; } = null!;

        public string? AttachmentId { get; set; }
    }
}
