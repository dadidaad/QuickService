using QuickServiceWebAPI.DTOs.Asset;
using QuickServiceWebAPI.DTOs.User;

namespace QuickServiceWebAPI.DTOs.AssetAssignment
{
    public class AssetAssignmentDTO
    {
        public string AssetAssignmentId { get; set; } = null!;

        public DateTime AssignedDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string AssetId { get; set; } = null!;

        public string AssignedTo { get; set; } = null!;

        public virtual AssetDTO AssetEntity { get; set; } = null!;

        public virtual UserDTO AssignedToUserEntity { get; set; } = null!;
    }
}
