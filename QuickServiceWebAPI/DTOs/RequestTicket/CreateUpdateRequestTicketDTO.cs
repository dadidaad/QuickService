using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.RequestTicket
{
    public class CreateUpdateRequestTicketDTO
    {
        [Required]
        public bool IsIncident { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string Priority { get; set; } = null!;

        public DateTime? LastUpdateAt { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        [MaxLength(10)]
        public string State { get; set; } = null!;

        [MaxLength(255)]
        public string? Tags { get; set; }

        public DateTime? ResolvedTime { get; set; }

        [Required]
        [MaxLength(10)]
        public string Impact { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string Urgency { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string RequesterId { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string ServiceItemId { get; set; } = null!;

        [MaxLength(10)]
        public string? AssignedTo { get; set; }

        [MaxLength(10)]
        public string? AssignedToGroup { get; set; }

        [Required]
        [MaxLength(10)]
        public string Slaid { get; set; } = null!;

        [MaxLength(10)]
        public string? AttachmentId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Title { get; set; } = null!;
    }
}
