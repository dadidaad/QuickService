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
            CreateMap<UpdateChangePropertiesDTO, Change>().IgnoreAllNonExisting();  
            CreateMap<Change, ChangeDTO>().
                ForMember(dest => dest.Requester, 
                opt => opt.MapFrom(src => src.Requester)).
                 ForMember(dest => dest.Group,
                opt => opt.MapFrom(src => src.Group)).
                 ForMember(dest => dest.Assigner,
                opt => opt.MapFrom(src => src.Assigner)).
                  ForMember(dest => dest.Attachment,
                opt => opt.MapFrom(src => src.Attachment));
        }
    }
}
