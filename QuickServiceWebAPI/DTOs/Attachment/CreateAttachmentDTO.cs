using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Attachment
{
    public class CreateAttachmentDTO
    {
        [Required]
        public string ReferenceId { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string ReferenceType { get; set; } = null!;


        public IFormFile AttachmentFile { get; set; } = null!;
    }
}
