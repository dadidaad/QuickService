using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Service
{
    public class CreateUpdateServiceDTO
    {
        [Required]
        [MaxLength(100)]
        public string ServiceName { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Description { get; set; }
        [Required]
        [MaxLength(10)]
        public string Impact { get; set; }
        [Required]
        [MaxLength(10)]
        public string HealthStatus { get; set; } 
        public string CreatedBy { get; set; } 
        public string ServiceTypeId { get; set; } 
        public string ManagedBy { get; set; } 
        public string? ManagedByGroup { get; set; }
    }
}
