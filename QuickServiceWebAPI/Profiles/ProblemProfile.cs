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
                 ForMember(dest => dest.ResponseTime,
                opt => opt.MapFrom(src => CalculateDatetime(src, true))).
                ForMember(dest => dest.ResolutionTime,
                opt => opt.MapFrom(src => CalculateDatetime(src, false))).
                 ForMember(dest => dest.Attachment,
                opt => opt.MapFrom(src => src.Attachment)).
                ForMember(dest => dest.Requester,
                opt => opt.MapFrom(src => src.Requester)).
                ForMember(dest => dest.RequestTickets,
                opt => opt.MapFrom(src => src.RequestTickets));
            CreateMap<CreateProblemDTO, Problem>().IgnoreAllNonExisting();
            CreateMap<UpdateProblemDTO, Problem>().IgnoreAllNonExisting();
        }

        private DateTime CalculateDatetime(Problem problem, bool isResponseDue)
        {
            Slametric slametric = problem.Sla.Slametrics.Where(s => problem.Priority == s.Priority).FirstOrDefault();
            return isResponseDue ? problem.CreatedTime + TimeSpan.FromTicks(slametric.ResponseTime)
                : problem.CreatedTime + TimeSpan.FromTicks(slametric.ResolutionTime);
        }
    }
}
