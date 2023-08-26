using AutoMapper;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class SlametricProfile : Profile
    {
        public SlametricProfile()
        {
            CreateMap<Slametric, SlametricDTO>()
                .ForMember(dest => dest.ResponseTime,
                opt => opt.MapFrom(src => TimeSpan.FromTicks(src.ResponseTime).TotalMinutes))
                .ForMember(dest => dest.ResolutionTime,
                opt => opt.MapFrom(src => TimeSpan.FromTicks(src.ResolutionTime).TotalMinutes));
            CreateMap<CreateSlametricDTO, Slametric>();
            CreateMap<Slametric, CreateSlametricDTO>().IgnoreAllNonExisting();
            CreateMap<UpdateSlametricsDTO, Slametric>()
                .ForMember(dest => dest.ResponseTime,
                opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.ResponseTime).Ticks))
                .ForMember(dest => dest.ResolutionTime,
                opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.ResolutionTime).Ticks));
        }
    }
}
