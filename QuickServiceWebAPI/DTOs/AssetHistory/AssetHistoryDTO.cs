using QuickServiceWebAPI.DTOs.Asset;

namespace QuickServiceWebAPI.DTOs.AssetHistory
{
    public class AssetHistoryDTO
    {
        public string AssetHistoryId { get; set; } = null!;

        public string Action { get; set; } = null!;

        public DateTime ActionTime { get; set; }

        public string? Description { get; set; }

        public string AssetId { get; set; } = null!;

        public virtual AssetDTO AssetEntity { get; set; } = null!;
    }
}
