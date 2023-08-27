using QuickServiceWebAPI.CustomAttributes;
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
        public List<String> RequestTicketIds { get; set; } = new List<String>();

        [Required]
        [MaxLength(10)]
        public string? AssigneeId { get; set; }

        [Required]
        [MaxLength(10)]
        public string? Slaid { get; set; }

        [Required]
        [MaxLength(10)]
        public string RequesterId { get; set; } = null!;

        [MaxLength]
        public string? ReasonForChange { get; set; }
    }
}
