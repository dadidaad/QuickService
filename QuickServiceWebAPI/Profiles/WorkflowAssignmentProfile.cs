using AutoMapper;
using QuickServiceWebAPI.DTOs.WorkflowAssignment;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class WorkflowAssignmentProfile : Profile
    {
        public WorkflowAssignmentProfile()
        {
            CreateMap<WorkflowAssignment, WorkflowAssignmentDTO>()
                .ForMember(dest => dest.CurrentTask,
                opt => opt.MapFrom(src => src.CurrentTask))
                .ForMember(dest => dest.Assignee,
                opt => opt.MapFrom(src => src.Assignee))
                .ForMember(dest => dest.Attachment,
                opt => opt.MapFrom(src => src.Attachment))
                .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => CheckStatusOfAssignment(src)));
            CreateMap<CheckWorkflowAssignmentDTO, WorkflowAssignment>().IgnoreAllNonExisting();
            CreateMap<AssignTaskToAgentDTO, WorkflowAssignment>();
        }


        public string CheckStatusOfAssignment(WorkflowAssignment workflowAssignment)
        {
            if(workflowAssignment.DueDate > DateTime.Now && !workflowAssignment.IsCompleted 
                && workflowAssignment.CurrentTask.Status != StatusWorkflowTaskEnum.Resolved.ToString())
            {
                return "On-time";
            }
            if((workflowAssignment.IsCompleted && workflowAssignment.CompletedTime > workflowAssignment.DueDate)
                || (!workflowAssignment.IsCompleted && workflowAssignment.DueDate < DateTime.Now) )
            {
                return "Overdue resolution time";
            }
            return "";
        }
    }
}
