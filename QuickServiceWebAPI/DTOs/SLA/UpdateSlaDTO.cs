using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Sla
{
    public class UpdateSlaDTO
    {
        [Required]
        [MaxLength(10)]
        public string SlaId { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Slaname { get; set; } = null!;

        [MaxLength(255)]
        public string? Description { get; set; }

        [MinLength(4, ErrorMessage = "The list must have 4 items."), MaxLength(4, ErrorMessage = "The list must have 4 items.")]
        [AllPrioritiesIncluded(typeof(PriorityEnum), nameof(Slametrics))]
        public List<UpdateSlametricsDTO> Slametrics { get; set; } = new List<UpdateSlametricsDTO>();
    }
}
