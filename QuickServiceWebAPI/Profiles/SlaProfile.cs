using AutoMapper;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class SlaProfile : Profile
    {
        public SlaProfile()
        {
            CreateMap<Sla, SlaDTO>()
                .ForMember(dest => dest.Slametrics,
                 opt => opt.MapFrom(src => src.Slametrics.ToList()));
            CreateMap<CreateSlaDTO, Sla>();
            CreateMap<UpdateSlaDTO, Sla>()
                .ForMember(dest => dest.Slametrics,
                 opt => opt.MapFrom(src => src.Slametrics));
        }
    }
}
