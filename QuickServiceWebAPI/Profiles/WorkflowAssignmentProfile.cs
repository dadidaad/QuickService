﻿using AutoMapper;
using QuickServiceWebAPI.DTOs.WorkflowAssignment;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;
using System.Reflection;

namespace QuickServiceWebAPI.Profiles
{
    public class WorkflowAssignmentProfile : Profile
    {
        public WorkflowAssignmentProfile()
        {
            CreateMap<WorkflowAssignment, WorkflowAssignmentDTO>()
                .ForMember(dest => dest.CurrentTask,
                opt => opt.MapFrom(src => src.CurrentTask))
                .ForMember(dest => dest.Assignee,
                opt => opt.MapFrom(src => src.Assignee))
                .ForMember(dest => dest.Attachment,
                opt => opt.MapFrom(src => src.Attachment));
            CreateMap<CheckWorkflowAssignmentDTO, WorkflowAssignment>().IgnoreAllNonExisting();
            CreateMap<AssignTaskToAgentDTO, WorkflowAssignment>();
        }
    }
}
