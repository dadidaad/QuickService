﻿using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.DTOs.WorkflowTransition;

namespace QuickServiceWebAPI.DTOs.WorkflowTask
{
    public class WorkflowTaskDTO
    {
        public string? WorkflowTaskId { get; set; }

        public string WorkflowTaskName { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? WorkflowId { get; set; }

        public virtual List<WorkflowTransitionDTO> WorkflowTransitionDTOFroms { get; set; } = new List<WorkflowTransitionDTO>();

        public virtual GroupDTO? GroupEntity { get; set; }

        public virtual UserDTO? UserEntity { get; set; }

        //public virtual WorkflowDTO? WorkflowEntity { get; set; }
    }
}
