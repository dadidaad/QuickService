using AutoMapper;
using QuickServiceWebAPI.DTOs.WorkflowAssignment;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class WorkflowAssignmentProfile : Profile
    {
        public WorkflowAssignmentProfile()
        {
            CreateMap<WorkflowAssignment, WorkflowAssignmentDTO>()
                .ForMember(dest => dest.WorkflowStepEntity,
                opt => opt.MapFrom(src => src.CurrentStep))
                .ForMember(dest => dest.RequestTicketEntity,
                opt => opt.MapFrom(src => src.RequestTicket))
                .ForMember(dest => dest.WorkflowEntity,
                opt => opt.MapFrom(src => src.Workflow));
            CreateMap<CreateUpdateWorkflowAssignmentDTO, WorkflowAssignment>();
        }
    }
}
