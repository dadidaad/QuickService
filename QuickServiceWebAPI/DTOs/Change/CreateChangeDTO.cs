using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Change
{
    public class CreateChangeDTO
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength]
        public string Description { get; set; } = null!;

        [Required]
        public string Status { get; set; } = null!;

        [Required]
        public string ChangeType { get; set; } = null!;

        [Required]
        public string Priority { get; set; } = null!;

        [Required]
        public string Impact { get; set; } = null!;

        [Required]
        public string Risk { get; set; } = null!;

        [MaxLength(10)]
        public string? GroupId { get; set; }

        [Required]
        [MaxLength (10)]
        public string RequesterId { get; set; } = null!;

        [MaxLength (10)] 
        public string? AssignerId { get; set; }

        [Required]
        [DatetimeRange("now", "PlannedEndDate", ErrorMessage = "PlannedStartDate must be in the future and before PlannedEndDate")]
        public DateTime PlannedStartDate { get; set; }

        [Required]
        public DateTime PlannedEndDate { get; set; }

        [FileSize(10 * 1024 * 1024)] // Maximum 10mB
        public IFormFile? AttachmentFile { get; set; }
    }
}
