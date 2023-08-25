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
                .MaxDepth(1)
                .PreserveReferences()
                .ForMember(dest => dest.RequestTicketId,
                 opt => opt.MapFrom(src => src.Relate != null ? src.Relate.RequestTicketId : null))
                .ForMember(dest => dest.RequestTicketId,
                 opt => opt.MapFrom(src => src.Relate != null ? src.Relate.Title : null))
                .ForMember(dest => dest.FromUserName,
                 opt => opt.MapFrom(src => src.FromUser != null ? string.Format($"{src.FromUser.FirstName} {src.FromUser.LastName}") : null))
                .ForMember(dest => dest.GroupName,
                 opt => opt.MapFrom(src => src.ToGroup != null ? src.ToGroup.GroupName : null));
        }
    }
}
