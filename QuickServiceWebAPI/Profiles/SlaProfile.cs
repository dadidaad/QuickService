using AutoMapper;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class SlaProfile : Profile
    {
        public SlaProfile() 
        {
            CreateMap<Sla, SlaDTO>();
            CreateMap<CreateUpdateSlaDTO, Sla>();
        }
    }
}
