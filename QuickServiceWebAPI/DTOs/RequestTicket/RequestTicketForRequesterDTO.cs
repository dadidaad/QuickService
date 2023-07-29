using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.DTOs.User;

namespace QuickServiceWebAPI.DTOs.RequestTicket
{
    public class RequestTicketForRequesterDTO
    {
        public string RequestTicketId { get; set; } = null!;

        public bool IsIncident { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? LastUpdateAt { get; set; }

        public virtual UserDTO? AssignedToUserEntity { get; set; }

        public virtual AttachmentDTO? AttachmentEntity { get; set; }

        public virtual ServiceItemDTO ServiceItemEntity { get; set; } = null!;
    }
}
