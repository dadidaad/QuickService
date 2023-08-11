using AutoMapper;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.RequestTicketHistory;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class RequestTicketHistoryProfile : Profile
    {
        public RequestTicketHistoryProfile()
        {
            CreateMap<RequestTicketHistory, RequestTicketHistoryDTO>()
                .ForMember(dest => dest.RequestTicketEntity,
                opt => opt.MapFrom(src => src.RequestTicket))
                .ForMember(dest => dest.UserEntity,
                opt => opt.MapFrom(src => src.UserId));
            CreateMap<CreateRequestTicketHistoryDTO, RequestTicketHistory>();
        }
    }
}
