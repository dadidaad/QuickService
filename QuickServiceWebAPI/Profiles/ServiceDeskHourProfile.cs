using AutoMapper;
using QuickServiceWebAPI.DTOs.ServiceDeskHour;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class ServiceDeskHourProfile : Profile
    {
        public ServiceDeskHourProfile()
        {
            CreateMap<ServiceDeskHour, ServiceDeskHourDTO>()
                .ForMember(dest => dest.BusinessHourEntity,
                opt => opt.MapFrom(src => src.BusinessHour));
            CreateMap<CreateUpdateServiceDeskHourDTO, ServiceDeskHour>();
        }
    }
}
