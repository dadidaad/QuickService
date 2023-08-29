using AutoMapper;
using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class WorkflowProfile : Profile
    {
        public WorkflowProfile()
        {
            CreateMap<Workflow, WorkflowDTO>()
                .ForMember(dest => dest.User,
                opt => opt.MapFrom(src => src.CreatedByNavigation))
                .ForMember(dest => dest.ServiceItemDTOs,
                opt => opt.MapFrom(src => src.ServiceItems));
            CreateMap<CreateUpdateWorkflowDTO, Workflow>();
            CreateMap<AssignWorkflowDTO, Workflow>();
        }
    }
}
