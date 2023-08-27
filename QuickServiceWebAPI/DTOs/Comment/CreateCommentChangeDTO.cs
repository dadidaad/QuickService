using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Comment
{
    public class CreateCommentChangeDTO
    {
        [Required]
        [MaxLength(255)]
        public string CommentText { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string CommentBy { get; set; } = null!;

        [Required]
        public bool IsInternal { get; set; }

        [Required]
        [MaxLength(10)]
        public string ChangeId { get; set; }
    }
}
