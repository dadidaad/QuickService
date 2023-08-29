using AutoMapper;
using QuickServiceWebAPI.DTOs.RequestTicketHistory;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class RequestTicketHistoryProfile : Profile
    {
        public RequestTicketHistoryProfile()
        {
            CreateMap<RequestTicketHistory, RequestTicketHistoryDTO>()
                .ForMember(dest => dest.UserEntity,
                opt => opt.MapFrom(src => src.User));
            CreateMap<CreateRequestTicketHistoryDTO, RequestTicketHistory>();
        }
    }
}
