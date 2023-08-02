using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Change
{
    public class PlanningDTO
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength]
        public string ReasonForChange { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MaxLength]
        public string ImpactPlanning { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MaxLength]
        public string RolloutPlan { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MaxLength]
        public string BackoutPlan { get; set; } = null!;
    }
}
