using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.DTOs.Service
{
    public class ServiceDTO
    {
        public string ServiceId { get; set; } = null!;

        public string ServiceName { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Impact { get; set; } = null!;

        public string HealthStatus { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;

        public string ServiceTypeId { get; set; } = null!;

        public string ManagedBy { get; set; } = null!;

        public string? ManagedByGroup { get; set; }

    }
}
