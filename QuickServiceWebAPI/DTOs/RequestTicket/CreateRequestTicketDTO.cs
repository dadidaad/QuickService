using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.RequestTicket
{
    public class CreateRequestTicketDTO
    {
        [Required]
        public bool IsIncident { get; set; }

        [MaxLength(500)]
        [CustomValidatorForIncident("IsIncident")]
        public string? Title { get; set; }

        [MaxLength(1000)]
        [CustomValidatorForIncident("IsIncident")]
        public string? Description { get; set; }

        [MaxLength(10)]
        public string? ServiceItemId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Requester { get; set; } = null!;

        [CustomValidatorForIncident("IsIncident")]
        [FileSize(10 * 1024 * 1024)]
        public IFormFile? Attachment { get; set; }
    }
}
