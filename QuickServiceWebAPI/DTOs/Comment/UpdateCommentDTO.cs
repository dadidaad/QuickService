using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Comment
{
    public class UpdateCommentDTO
    {
        [Required]
        [MaxLength(255)]
        public string CommentText { get; set; } = null!;
    }
}
