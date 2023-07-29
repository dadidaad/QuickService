using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.RequestTicket
{
    public class UpdateRequestTicketDTO
    {
        [Required]
        [MaxLength(10)]
        public string RequestTicketId { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string Status { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string Priority { get; set; } = null!;

        [MaxLength(255)]
        public string? Tags { get; set; }

        [Required]
        [MaxLength(10)]
        public string Impact { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string Urgency { get; set; } = null!;

        [MaxLength(10)]
        public string? AssignedTo { get; set; }
    }
}
