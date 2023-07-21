using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Service
{
    public class CreateUpdateServiceDTO
    {
        [Required]
        [MaxLength(100)]
        public string ServiceName { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(10)]
        public string Impact { get; set; }

        [Required]
        [MaxLength(10)]
        public string HealthStatus { get; set; }

        [Required]
        [MaxLength(10)]
        public string CreatedBy { get; set; }

        [Required]
        [MaxLength(10)]
        public string ServiceTypeId { get; set; }

        [Required]
        [MaxLength(10)]
        public string ManagedBy { get; set; }

        [Required]
        [MaxLength(10)]
        public string? ManagedByGroup { get; set; }
    }
}
