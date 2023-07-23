using AutoMapper;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class RequestTicketProfile : Profile
    {
        public RequestTicketProfile()
        {
            CreateMap<RequestTicket, RequestTicketDTO>()
                .ForMember(dest => dest.GroupEntity,
                opt => opt.MapFrom(src => src.AssignedToGroupNavigation))
                .ForMember(dest => dest.AssignedToUserEntity,
                opt => opt.MapFrom(src => src.AssignedToNavigation))
                .ForMember(dest => dest.AttachmentEntity,
                opt => opt.MapFrom(src => src.Attachment))
                .ForMember(dest => dest.RequesterUserEntity,
                opt => opt.MapFrom(src => src.Requester))
                .ForMember(dest => dest.ServiceItemEntity,
                opt => opt.MapFrom(src => src.ServiceItem))
                .ForMember(dest => dest.SlaEntity,
                opt => opt.MapFrom(src => src.Sla));
            CreateMap<CreateUpdateRequestTicketDTO, RequestTicket>();
        }
    }
}
