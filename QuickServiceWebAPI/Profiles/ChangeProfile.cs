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
                 ForMember(dest => dest.Sla,
                opt => opt.MapFrom(src => src.Sla));
        }
    }
}
