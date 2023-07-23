using AutoMapper;
using QuickServiceWebAPI.DTOs.Comment;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.RequestTicketExt;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class RequestTicketExtProfile : Profile
    {
        public RequestTicketExtProfile()
        {
            CreateMap<RequestTicketExt, RequestTicketExtDTO>()
                .ForMember(dest => dest.FieldEntity,
                opt => opt.MapFrom(src => src.Field))
                .ForMember(dest => dest.RequestTicketEntity,
                opt => opt.MapFrom(src => src.Ticket));
            CreateMap<CreateUpdateRequestTicketExtDTO, RequestTicketExt>();
        }
    }
}
