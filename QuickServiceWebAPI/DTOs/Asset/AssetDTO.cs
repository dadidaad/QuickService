namespace QuickServiceWebAPI.DTOs.Asset
{
    public class AssetDTO
    {
        public string AssetId { get; set; } = null!;

        public string AssetName { get; set; } = null!;

        public string AssetType { get; set; } = null!;

        public string? Description { get; set; }

        public string? Manufacturer { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string? Location { get; set; }

        public string Status { get; set; } = null!;
    }
}
