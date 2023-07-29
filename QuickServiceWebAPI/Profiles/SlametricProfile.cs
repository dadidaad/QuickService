using AutoMapper;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class SlametricProfile : Profile
    {
        public SlametricProfile()
        {
            CreateMap<Slametric, SlametricDTO>()
                .ForMember(dest => dest.BusinessHourEntity,
                opt => opt.MapFrom(src => src.BusinessHour))
                .ForMember(dest => dest.SlaEntity,
                opt => opt.MapFrom(src => src.Sla));
            CreateMap<CreateUpdateSlametricDTO, Slametric>();
        }
    }
}
