using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.RequestTicket
{
    public class RequesterResquestDTO
    {
        [Required]
        [MaxLength(10)]
        public string Requester { get; set; } = null!;

        [MaxLength(10)]
        public string? RequestTicketId { get; set; }
    }
}
