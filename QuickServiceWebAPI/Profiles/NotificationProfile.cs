using AutoMapper;
using QuickServiceWebAPI.DTOs.Notification;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationDTO>()
                .ForMember(dest => dest.Relate,
                 opt => opt.MapFrom(src => src.Relate))
                .ForMember(dest => dest.FromUser,
                 opt => opt.MapFrom(src => src.FromUser))
                .ForMember(dest => dest.ToGroup,
                 opt => opt.MapFrom(src => src.ToGroup));
        }
    }
}
