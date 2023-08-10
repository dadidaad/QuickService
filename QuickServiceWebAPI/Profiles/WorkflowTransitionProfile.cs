using AutoMapper;
using QuickServiceWebAPI.DTOs.WorkflowTransition;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class WorkflowTransitionProfile : Profile
    {
        public WorkflowTransitionProfile()
        {
            CreateMap<WorkflowTransitionDTO, WorkflowTransition>();
            CreateMap<WorkflowTransition, WorkflowTransitionDTO>();
            CreateMap<DeleteWorkflowTransitionDTO, WorkflowTransition>();
        }
    }
}
