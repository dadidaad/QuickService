using QuickServiceWebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.RequestTicket
{
    public class UpdateRequestTicketDTO
    {
        [Required]
        [MaxLength(10)]
        public string RequestTicketId { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(StatusEnum))]
        public string Status { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(ImpactEnum))]
        public string Impact { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(UrgencyEnum))]
        public string Urgency { get; set; } = null!;
    }
}
