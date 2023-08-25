using QuickServiceWebAPI.DTOs.User;

namespace QuickServiceWebAPI.DTOs.RequestTicketHistory
{
    public class RequestTicketHistoryDTO
    {
        public string RequestTicketId { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime LastUpdate { get; set; }

        public string UserId { get; set; } = null!;

        public virtual UserDTO UserEntity { get; set; } = null!;
    }
}
