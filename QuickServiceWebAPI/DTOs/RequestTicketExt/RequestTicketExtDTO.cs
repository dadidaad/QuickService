using QuickServiceWebAPI.DTOs.CustomField;
using QuickServiceWebAPI.DTOs.RequestTicket;

namespace QuickServiceWebAPI.DTOs.RequestTicketExt
{
    public class RequestTicketExtDTO
    {
        public string RequestTicketExId { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string TicketId { get; set; } = null!;

        public string FieldId { get; set; } = null!;

        public string? FieldValue { get; set; }

        public virtual CustomFieldDTO FieldEntity { get; set; } = null!;

        public virtual RequestTicketDTO RequestTicketEntity { get; set; } = null!;
    }
}
