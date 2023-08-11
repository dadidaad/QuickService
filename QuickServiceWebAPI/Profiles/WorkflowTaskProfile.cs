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
                .ForMember(dest => dest.WorkflowEntity,
                opt => opt.MapFrom(src => src.Workflow));
            CreateMap<CreateUpdateWorkflowTaskDTO, WorkflowTask>();
        }
    }
}
