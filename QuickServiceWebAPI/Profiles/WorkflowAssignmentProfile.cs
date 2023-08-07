using AutoMapper;
using QuickServiceWebAPI.DTOs.WorkflowAssignment;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class WorkflowAssignmentProfile : Profile
    {
        public WorkflowAssignmentProfile()
        {
            CreateMap<WorkflowAssignment, WorkflowAssignmentDTO>()
                .ForMember(dest => dest.WorkflowStepEntity,
                opt => opt.MapFrom(src => src.CurrentStep))
                .ForMember(dest => dest.WorkflowEntity,
                opt => opt.MapFrom(src => src.Workflow));
            CreateMap<CreateUpdateWorkflowAssignmentDTO, WorkflowAssignment>();
            CreateMap<CheckWorkflowAssignmentDTO, WorkflowAssignment>().IgnoreAllNonExisting();
        }
    }
}
