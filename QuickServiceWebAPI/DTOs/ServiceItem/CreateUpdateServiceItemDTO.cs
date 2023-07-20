﻿using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.ServiceItem
{
    public class CreateUpdateServiceItemDTO
    {
        [Required]
        [MaxLength(100)]
        public string ServiceItemName { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string ShortDescription { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public int EstimatedDelivery { get; set; }

        public bool Status { get; set; }

        [Required]
        public string ServiceCategoryId { get; set; } = null!;

        [Display(Name = "Icon")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile? IconImage { get; set; }
    }
}
