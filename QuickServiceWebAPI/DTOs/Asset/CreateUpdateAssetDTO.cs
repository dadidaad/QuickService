using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Asset
{
    public class CreateUpdateAssetDTO
    {
        [Required]
        [MaxLength(100)]
        public string AssetName { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string AssetType { get; set; } = null!;

        [MaxLength(200)]
        public string? Description { get; set; }

        [MaxLength(50)]
        public string? Manufacturer { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [MaxLength(50)]
        public string? Location { get; set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; set; } = null!;
    }
}
