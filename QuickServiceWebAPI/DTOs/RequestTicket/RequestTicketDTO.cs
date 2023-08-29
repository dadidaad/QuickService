using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.DTOs.User;

namespace QuickServiceWebAPI.DTOs.RequestTicket
{
    public class RequestTicketDTO
    {
        public string RequestTicketId { get; set; } = null!;

        public bool IsIncident { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; } = null!;

        public string Priority { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? LastUpdateAt { get; set; }

        public DateTime? FirstResponseDue { get; set; }

        public DateTime? FirstResolutionDue { get; set; }

        public string State { get; set; } = null!;

        public string? Tags { get; set; }

        public DateTime? ResolvedTime { get; set; }

        public string Impact { get; set; } = null!;

        public string Urgency { get; set; } = null!;

        public string RequesterId { get; set; } = null!;

        public string ServiceItemId { get; set; } = null!;

        public string? AssignedTo { get; set; }

        public string Title { get; set; } = null!;

        //public virtual SlaDTO? Sla { get; set; }

        public virtual UserDTO? AssignedToUserEntity { get; set; }

        public virtual AttachmentDTO? AttachmentEntity { get; set; }

        public virtual UserDTO RequesterUserEntity { get; set; } = null!;

        public virtual ServiceItemDTO ServiceItemEntity { get; set; } = null!;


    }
}
