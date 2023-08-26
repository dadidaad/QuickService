namespace QuickServiceWebAPI.Models;

public partial class RequestTicketExt
{
    public string RequestTicketExId { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string TicketId { get; set; } = null!;

    public string FieldId { get; set; } = null!;

    public string? FieldValue { get; set; }

    public virtual CustomField Field { get; set; } = null!;

    public virtual RequestTicket Ticket { get; set; } = null!;
}
