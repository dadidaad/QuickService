using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.AssetAssignment
{
    public class CreateUpdateAssetAssignmentDTO
    {
        [Required]
        public DateTime AssignedDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [Required]
        [MaxLength(10)]
        public string AssetId { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string AssignedTo { get; set; } = null!;
    }
}
