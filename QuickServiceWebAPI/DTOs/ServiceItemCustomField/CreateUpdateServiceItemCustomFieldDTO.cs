using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.ServiceItemCustomField
{
    public class CreateUpdateServiceItemCustomFieldDTO
    {
        [Required]
        [MaxLength(10)]
        public string ServiceItemId { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string CustomFieldId { get; set; } = null!;

        public bool? Mandatory { get; set; }
    }
}
