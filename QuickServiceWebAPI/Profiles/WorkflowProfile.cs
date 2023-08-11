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
                opt => opt.MapFrom(src => src.CreatedByNavigation));
            CreateMap<CreateUpdateWorkflowDTO, Workflow>();
            CreateMap<AssignWorkflowDTO, Workflow>();
        }
    }
}
