﻿using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Workflow
{
    public class AssignWorkflowDTO
    {
        [Required]
        [MaxLength(10)]
        public string WorkflowId { get; set; } = null!;

        [MaxLength(10)]
        public string ServiceItemId { get; set; } = null!;

    }
}
