using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Sla
{
    public class CreateSlaDTO
    {
        [Required]
        [MaxLength(100)]
        public string Slaname { get; set; } = null!;

        [MaxLength(255)]
        public string? Description { get; set; }

    }
}
