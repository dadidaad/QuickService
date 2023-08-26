using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Problem
{
    public class UpdateProblemDTO
    {
        [Required]
        [MaxLength(10)]
        public string ProblemId { get; set; } = null!;

        [Required]
        [MaxLength(250)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength]
        public string Description { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(StatusEnum))]
        public string Status { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(PriorityEnum))]
        public string Priority { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(ImpactEnum))]
        public string Impact { get; set; } = null!;
        [MaxLength]
        public string? RootCause { get; set; }

        [Required]
        [MaxLength(10)]
        public string? AssigneeId { get; set; }

        [FileSize(10 * 1024 * 1024)]
        public IFormFile? AttachmentFile { get; set; }
    }
}
