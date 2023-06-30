using AutoMapper;
using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class WorkflowProfile : Profile
    {
        public WorkflowProfile()
        {
            CreateMap<Workflow, WorkflowDTO>();
            CreateMap<CreateUpdateWorkflowDTO, Workflow>();
        }
    }
}
