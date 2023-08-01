using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Change
{
    public class UpdateChangePropertiesDTO
    {
        [Required]
        [MaxLength(10)]
        public string ChangeId { get; set; } = null!;
        [Required]
        [EnumDataType(typeof(StatusChangeEnum))]
        public string Status { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(ChangeTypeEnum))]
        public string ChangeType { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(PriorityEnum))]
        public string Priority { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(ImpactEnum))]
        public string Impact { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(RiskEnum))]
        public string Risk { get; set; } = null!;

        [MaxLength(10)]
        public string? GroupId { get; set; }


        [MaxLength(10)]
        public string? AssignerId { get; set; }

        public PlanningDTO? PlanningDTO { get; set; } 

        [Required]
        [DatetimeRange("now", "PlannedEndDate", ErrorMessage = "PlannedStartDate must be in the future and before PlannedEndDate")]
        public DateTime PlannedStartDate { get; set; }

        [Required]
        public DateTime PlannedEndDate { get; set; }
    }
}
