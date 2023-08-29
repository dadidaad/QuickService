using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Attachment
{
    public class CreateAttachmentDTO
    {
        [Required]
        [FileSize(10 * 1024 * 1024)]
        public IFormFile AttachmentFile { get; set; } = null!;
    }
}
