namespace QuickServiceWebAPI.DTOs.Service
{
    public class CreateUpdateServiceDTO
    {
        public string ServiceName { get; set; } 
        public string? Description { get; set; }
        public string Impact { get; set; } 
        public string HealthStatus { get; set; } 
        public string CreatedBy { get; set; } 
        public string ServiceTypeId { get; set; } 
        public string ManagedBy { get; set; } 
        public string? ManagedByGroup { get; set; }
    }
}
