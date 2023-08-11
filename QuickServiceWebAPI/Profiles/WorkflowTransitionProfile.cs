using AutoMapper;
using QuickServiceWebAPI.DTOs.WorkflowTransition;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class WorkflowTransitionProfile : Profile
    {
        public WorkflowTransitionProfile()
        {
            CreateMap<CreateWorkflowTransitionDTO, WorkflowTransition>();
            CreateMap<WorkflowTransition, WorkflowTransitionDTO>()
                .ForMember(dest => dest.FromWorkflowTaskNavigation,
                opt => opt.MapFrom(src => src.FromWorkflowTaskNavigation))
                .ForMember(dest => dest.ToWorkflowTaskNavigation,
                opt => opt.MapFrom(src => src.ToWorkflowTaskNavigation));
            CreateMap<DeleteWorkflowTransitionDTO, WorkflowTransition>();
        }
    }
}
