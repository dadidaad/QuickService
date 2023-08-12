using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.RequestTicket
{
    public class CreateRequestTicketDTO
    {
        public bool IsIncident { get; set; }

        [MaxLength(500)]
        [Required]
        public string Title { get; set; } = null!;

        [MaxLength(1000)]
        [CustomValidatorForIncident("IsIncident")]
        public string? Description { get; set; }

        [MaxLength(10)]
        [CustomValidatorForIncident("IsIncident")]
        public string? ServiceItemId { get; set; }

        [Required]
        [MaxLength(100)]
        public string RequesterEmail { get; set; } = null!;

        [FileSize(10 * 1024 * 1024)]
        public IFormFile? Attachment { get; set; }
    }
}
