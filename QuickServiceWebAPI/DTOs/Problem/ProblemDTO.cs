using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.Group;
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

        public DateTime DueTime { get; set; }

        public string? GroupId { get; set; }

        public string? AssignerId { get; set; }

        public string RequesterId { get; set; } = null!;

        public string? RootCause { get; set; }

        public string? ImpactAnalysis { get; set; }

        public string? Symptoms { get; set; }

        public string? AttachmentId { get; set; }

        public virtual UserDTO? AssignerEntity { get; set; }

        public virtual AttachmentDTO? AttachmentEntity { get; set; }

        public virtual GroupDTO? GroupEntity { get; set; }

        public virtual UserDTO RequesterEntity { get; set; } = null!;
    }
}
