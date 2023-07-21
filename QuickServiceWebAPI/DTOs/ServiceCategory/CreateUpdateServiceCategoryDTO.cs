using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.ServiceCategory
{
    public class CreateUpdateServiceCategoryDTO
    {
        [Required]
        [MaxLength(100)]
        public string ServiceCategoryName { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
