using AutoMapper;
using QuickServiceWebAPI.DTOs.Change;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class ChangeProfile : Profile
    {
        public ChangeProfile()
        {
            CreateMap<CreateChangeDTO, Change>().IgnoreAllNonExisting();
            CreateMap<UpdateChangeDTO, Change>().IgnoreAllNonExisting();
            CreateMap<Change, ChangeDTO>().
                ForMember(dest => dest.Assignee,
                opt => opt.MapFrom(src => src.Assignee)).
                 ForMember(dest => dest.Attachment,
                opt => opt.MapFrom(src => src.Attachment)).
                 ForMember(dest => dest.ResponseTime,
                opt => opt.MapFrom(src => CalculateDatetime(src, true))).
                ForMember(dest => dest.ResolutionTime,
                opt => opt.MapFrom(src => CalculateDatetime(src, false))).
                ForMember(dest => dest.Requester,
                opt => opt.MapFrom(src => src.Requester)).
                 ForMember(dest => dest.RequestTickets,
                opt => opt.MapFrom(src => src.RequestTickets));
        }


        private DateTime CalculateDatetime(Change change, bool isResponseDue)
        {
            Slametric slametric = change.Sla.Slametrics.Where(s => change.Priority == s.Priority).FirstOrDefault();
            return isResponseDue ? change.CreatedTime + TimeSpan.FromTicks(slametric.ResponseTime)
                : change.CreatedTime + TimeSpan.FromTicks(slametric.ResolutionTime);
        }
    }
}
