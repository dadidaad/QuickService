﻿using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.CustomField
{
    public class CreateUpdateCustomField
    {
        [Required]
        [MaxLength(50)]
        public string FieldCode { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string FieldName { get; set; } = null!;

        [MaxLength(300)]
        public string? FieldDescription { get; set; }

        [Required]
        [MaxLength(10)]
        public string FieldType { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string ValType { get; set; } = null!;

        public int? MinVal { get; set; }

        public int? MaxVal { get; set; }

        public int? MinLength { get; set; }

        public int? MaxLength { get; set; }

        [MaxLength(500)]
        public string? DefaultValue { get; set; }

        [MaxLength(500)]
        public string? ListOfValue { get; set; }

        [MaxLength(2500)]
        public string? ListOfValueDisplay { get; set; }
    }
}