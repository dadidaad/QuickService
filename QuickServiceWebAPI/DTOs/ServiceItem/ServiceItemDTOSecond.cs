namespace QuickServiceWebAPI.DTOs.ServiceItem
{
    public class ServiceItemDTOSecond
    {
        public string ServiceItemId { get; set; } = null!;

        public string ServiceItemName { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public string? Description { get; set; }

        public int EstimatedDelivery { get; set; }

        public string Status { get; set; } = null!;

        public string? AttachmentId { get; set; }

        public string? IconDisplay { get; set; }
    }
}
