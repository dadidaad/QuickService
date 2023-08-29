using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.AssetHistory
{
    public class CreateUpdateAssetHistoryDTO
    {
        [Required]
        [MaxLength(10)]
        public string Action { get; set; } = null!;

        [Required]
        public DateTime ActionTime { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(10)]
        public string AssetId { get; set; } = null!;
    }
}
