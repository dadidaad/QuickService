using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.RequestTicketExt
{
    public class CreateUpdateRequestTicketExtDTO
    {
        [Required]
        [MaxLength(10)]
        public string TicketId { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string FieldId { get; set; } = null!;

        [MaxLength(2000)]
        public string? FieldValue { get; set; }
    }
}
