namespace QuickServiceWebAPI.DTOs.Sla
{
    public class SlaDTO
    {
        public string? Slaid { get; set; }

        public string? Slaname { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }
    }
}
