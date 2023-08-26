using AutoMapper;
using QuickServiceWebAPI.DTOs.Problem;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class ProblemProfile : Profile
    {
        public ProblemProfile()
        {
            CreateMap<Problem, ProblemDTO>().
                ForMember(dest => dest.Assignee,
                opt => opt.MapFrom(src => src.Assignee)).
                 ForMember(dest => dest.Sla,
                opt => opt.MapFrom(src => src.Sla)).
                 ForMember(dest => dest.Attachment,
                opt => opt.MapFrom(src => src.Attachment));
            CreateMap<CreateProblemDTO, Problem>().IgnoreAllNonExisting();
            CreateMap<UpdateProblemDTO, Problem>().IgnoreAllNonExisting();
        }
    }
}
