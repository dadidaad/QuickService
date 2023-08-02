using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Change
{
    public class UpdateChangeDTO
    {
        [Required]
        [MaxLength(10)]
        public string ChangeId { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(250)]
        public string Title { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MaxLength]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string RequesterId { get; set; } = null!;

        [FileSize(10 * 1024 * 1024)] // Maximum 10mB
        public IFormFile? AttachmentFile { get; set; }
    }
}
