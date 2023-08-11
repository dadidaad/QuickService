using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.WorkflowTask
{
    public class WorkflowTaskDTO
    {
        public string? WorkflowStepId { get; set; }

        public string WorkflowTaskName { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? WorkflowId { get; set; }

        public virtual WorkflowDTO WorkflowEntity { get; set; } = null!;
    }
}
