using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.DTOs.User;

namespace QuickServiceWebAPI.DTOs.Problem
{
    public class ProblemDTO
    {
        public string ProblemId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string Priority { get; set; } = null!;

        public string Impact { get; set; } = null!;

        public string? AssigneeId { get; set; }

        public string? AttachmentId { get; set; }

        public string Slaid { get; set; } = null!;
        public string RequesterId { get; set; } = null!;

        public string? RootCause { get; set; }

        public virtual UserDTO? Assignee { get; set; }
        public virtual AttachmentDTO? Attachment { get; set; }
        public virtual SlaDTO Sla { get; set; } = null!;
        public virtual UserDTO? Requester { get; set; }
        public virtual ICollection<RequestTicketDTO> RequestTickets { get; set; } = new List<RequestTicketDTO>();
    }
}
