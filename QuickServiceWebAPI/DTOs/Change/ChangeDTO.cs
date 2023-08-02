using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.DTOs.Change
{
    public class ChangeDTO
    {
        public string ChangeId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string ChangeType { get; set; } = null!;

        public string Priority { get; set; } = null!;

        public string Impact { get; set; } = null!;

        public string Risk { get; set; } = null!;

        public string? GroupId { get; set; }

        public string RequesterId { get; set; } = null!;

        public string? AssignerId { get; set; }

        public DateTime PlannedStartDate { get; set; }

        public DateTime PlannedEndDate { get; set; }

        public string? ReasonForChange { get; set; }

        public string? ImpactPlanning { get; set; }

        public string? RolloutPlan { get; set; }

        public string? BackoutPlan { get; set; }

        public string? ProblemId { get; set; }

        public string? AttachmentId { get; set; }

        public bool IsApprovedByCab { get; set; }

        public DateTime CreatedTime { get; set; }

        public virtual UserDTO? Assigner { get; set; }

        public virtual AttachmentDTO? Attachment { get; set; }

        public virtual GroupDTO? Group { get; set; }

        //public virtual Problem? Problem { get; set; }

        public virtual ICollection<RequestTicketDTO> RequestTickets { get; set; } = new List<RequestTicketDTO>();

        public virtual UserDTO Requester { get; set; } = null!;
    }
}
