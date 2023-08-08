using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Comment
{
    public class CreateCommentDTO
    {
        [Required]
        [MaxLength(255)]
        public string CommentText { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string CommentBy { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string RequestTicketId { get; set; } = null!;

        [MaxLength(10)]
        public string? AttachmentId { get; set; }
    }
}
