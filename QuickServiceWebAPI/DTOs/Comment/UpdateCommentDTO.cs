using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Comment
{
    public class UpdateCommentDTO
    {
        [Required]
        [MaxLength(10)]
        public string CommentId { get; set; } = null!;
        [Required]
        [MaxLength(255)]
        public string CommentText { get; set; } = null!;
    }
}
