using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;

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

        public DateTime? DueDate { get; set; }

        public string State { get; set; } = null!;

        public string? Tags { get; set; }

        public DateTime? ResolvedTime { get; set; }

        public string Impact { get; set; } = null!;

        public string Urgency { get; set; } = null!;

        public string RequesterId { get; set; } = null!;

        public string ServiceItemId { get; set; } = null!;

        public string? AssignedTo { get; set; }

        public string? AssignedToGroup { get; set; }

        public string Slaid { get; set; } = null!;

        public string? AttachmentId { get; set; }

        public string Title { get; set; } = null!;

        public virtual GroupDTO? GroupEntity { get; set; }

        public virtual UserDTO? AssignedToUserEntity { get; set; }

        public virtual AttachmentDTO? AttachmentEntity { get; set; }

        public virtual UserDTO RequesterUserEntity { get; set; } = null!;

        public virtual ServiceItemDTO ServiceItemEntity { get; set; } = null!;

        public virtual SlaDTO SlaEntity { get; set; } = null!;

    }
}
