using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Sla
{
    public class CreateUpdateSlaDTO
    {
        [Required]
        [MaxLength(100)]
        public string Slaname { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

    }
}
