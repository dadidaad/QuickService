using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.Models.Enums;
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

        [MinLength(4, ErrorMessage = "The list must have 4 items."), MaxLength(4, ErrorMessage = "The list must have 4 items.")]
        [AllPrioritiesIncluded(typeof(PriorityEnum), nameof(SlametricsDetails))]
        public List<CreateSlametricDTO> SlametricsDetails { get; set; } = new List<CreateSlametricDTO>();

    }
}
