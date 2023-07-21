using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Attachment
{
    public class CreateUpdateAttachmentDTO
    {
        [Required]
        public string ReferenceId { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string ReferenceType { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Filename { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string FilePath { get; set; } = null!;

        [Required]
        public int FileSize { get; set; }

    }
}
