namespace QuickServiceWebAPI.DTOs.Sla
{
    public class CreateUpdateSlaDTO
    {
        public string Slaname { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}
