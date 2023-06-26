using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.ServiceType
{
    public class CreateUpdateServiceTypeDTO
    {
        [Required]
        [MaxLength(100)]
        public string ServiceTypeName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
