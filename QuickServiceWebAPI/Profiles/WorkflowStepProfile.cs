using AutoMapper;
using QuickServiceWebAPI.DTOs.WorkflowStep;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class WorkflowStepProfile : Profile
    {
        public WorkflowStepProfile() 
        {
            CreateMap<WorkflowStep, WorkflowStepDTO>();
            CreateMap<CreateUpdateWorkflowStepDTO, WorkflowStep>();
        }
    }
}
