using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.DTOs.User;

namespace QuickServiceWebAPI.DTOs.Change
{
    public class ChangeDTO
    {
        public string ChangeId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string Priority { get; set; } = null!;

        public string Impact { get; set; } = null!;

        public DateTime ResolutionTime { get; set; }

        public DateTime ResponseTime { get; set; }

        public string? ReasonForChange { get; set; }

        public virtual UserDTO? Assignee { get; set; }

        public virtual AttachmentDTO? Attachment { get; set; }

        public virtual ICollection<RequestTicketDTO> RequestTickets { get; set; } = new List<RequestTicketDTO>();

        public virtual UserDTO? Requester { get; set; }
    }
}
