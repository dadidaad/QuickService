using AutoMapper;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class RequestTicketProfile : Profile
    {
        public RequestTicketProfile()
        {
            CreateMap<RequestTicket, RequestTicketDTO>()
                .ForMember(dest => dest.AssignedToUserEntity,
                opt => opt.MapFrom(src => src.AssignedToNavigation))
                .ForMember(dest => dest.AttachmentEntity,
                opt => opt.MapFrom(src => src.Attachment))
                .ForMember(dest => dest.RequesterUserEntity,
                opt => opt.MapFrom(src => src.Requester))
                .ForMember(dest => dest.ServiceItemEntity,
                opt => opt.MapFrom(src => src.ServiceItem))
                .ForMember(dest => dest.FirstResponseDue,
                opt => opt.MapFrom(src => CalculateDatetime(src, true)))
                .ForMember(dest => dest.FirstResolutionDue,
                opt => opt.MapFrom(src => CalculateDatetime(src, false)));
            CreateMap<CreateRequestTicketDTO, RequestTicket>().IgnoreAllNonExisting();
            CreateMap<RequestTicket, RequestTicketForRequesterDTO>()
                .ForMember(dest => dest.AssignedToUserEntity,
                opt => opt.MapFrom(src => src.AssignedToNavigation))
                .ForMember(dest => dest.AttachmentEntity,
                opt => opt.MapFrom(src => src.Attachment))
                .ForMember(dest => dest.ServiceItemEntity,
                opt => opt.MapFrom(src => src.ServiceItem));
        }

        private DateTime CalculateDatetime(RequestTicket requestTicket, bool isResponseDue)
        {
            Slametric slametric = requestTicket.Sla.Slametrics.Where(s => requestTicket.Priority == s.Piority).FirstOrDefault();
            return isResponseDue ? requestTicket.CreatedAt + TimeSpan.FromTicks(slametric.ResponseTime)
                : requestTicket.CreatedAt + TimeSpan.FromTicks(slametric.ResolutionTime);
        }
    }
}
