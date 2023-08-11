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
                opt => opt.MapFrom(src => src.Sla))
                .ForMember(dest => dest.ResponseTime,
                opt => opt.MapFrom(src => TimeSpan.FromTicks(src.ResponseTime).TotalMinutes))
                .ForMember(dest => dest.ResolutionTime,
                opt => opt.MapFrom(src => TimeSpan.FromTicks(src.ResolutionTime).TotalMinutes));
            CreateMap<CreateUpdateSlametricDTO, Slametric>()
                .ForMember(dest => dest.ResponseTime,
                opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.ResponseTime).Ticks))
                .ForMember(dest => dest.ResolutionTime,
                opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.ResolutionTime).Ticks));
        }
    }
}
