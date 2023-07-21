using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.User;

namespace QuickServiceWebAPI.DTOs.Comment
{
    public class CommentDTO
    {
        public string CommentId { get; set; } = null!;

        public string CommentText { get; set; } = null!;

        public DateTime CommentTime { get; set; }

        public bool IsInternal { get; set; }

        public string CommentBy { get; set; } = null!;

        public string RequestTicketId { get; set; } = null!;

        public string? AttachmentId { get; set; }

        public virtual AttachmentDTO? AttachmentEntity { get; set; }

        public virtual UserDTO CommentByUserEntity { get; set; } = null!;

        public virtual RequestTicketDTO RequestTicketEntity { get; set; } = null!;
    }
}
