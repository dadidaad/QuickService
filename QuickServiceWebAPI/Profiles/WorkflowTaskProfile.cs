using AutoMapper;
using QuickServiceWebAPI.DTOs.WorkflowTask;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class WorkflowTaskProfile : Profile
    {
        public WorkflowTaskProfile()
        {
            CreateMap<WorkflowTask, WorkflowTaskDTO>()
                .ForMember(dest => dest.UserEntity,
                opt => opt.MapFrom(src => src.Assigner))
                .ForMember(dest => dest.GroupEntity,
                opt => opt.MapFrom(src => src.Group))
                .ForMember(dest => dest.WorkflowTransitionDTOFroms,
                opt => opt.MapFrom(src => src.WorkflowTransitionFromWorkflowTaskNavigations));
            CreateMap<CreateUpdateWorkflowTaskDTO, WorkflowTask>();
        }
    }
}
