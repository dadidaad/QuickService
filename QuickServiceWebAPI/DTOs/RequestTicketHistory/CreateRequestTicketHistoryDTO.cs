namespace QuickServiceWebAPI.DTOs.RequestTicketHistory
{
    public class CreateRequestTicketHistoryDTO
    {
        public string RequestTicketId { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string UserId { get; set; } = null!;
    }
}
