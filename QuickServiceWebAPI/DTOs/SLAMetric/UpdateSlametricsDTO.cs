﻿using QuickServiceWebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.SLAMetric
{
    public class UpdateSlametricsDTO
    {
        [Required]
        [MaxLength(10)]
        public string SlametricId { get; set; } = null!;

        [EnumDataType(typeof(PriorityEnum))]
        public string Priority { get; set; } = null!;

        public long ResponseTime { get; set; }

        public long ResolutionTime { get; set; }

        public string SlaId { get; set; } = null!;
    }
}
