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
                .ForMember(dest => dest.CurrentStep,
                opt => opt.MapFrom(src => src.CurrentStep))
                .ForMember(dest => dest.Attachment,
                opt => opt.MapFrom(src => src.Attachment));
            CreateMap<CheckWorkflowAssignmentDTO, WorkflowAssignment>().IgnoreAllNonExisting();
            CreateMap<RejectWorkflowTaskDTO, WorkflowAssignment>().IgnoreAllNonExisting();
        }
    }
}
