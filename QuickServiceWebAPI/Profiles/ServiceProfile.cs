using AutoMapper;
using QuickServiceWebAPI.DTOs.Service;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceDTO>()
                .ForMember(dest => dest.CreatedByUserEntity,
                opt => opt.MapFrom(src => src.CreatedByNavigation))
                .ForMember(dest => dest.ManagedByGroupEntity,
                opt => opt.MapFrom(src => src.ManagedByGroupNavigation))
                .ForMember(dest => dest.ManagedByUserEntity,
                opt => opt.MapFrom(src => src.ManagedByNavigation))
                .ForMember(dest => dest.ServiceTypeEntity,
                opt => opt.MapFrom(src => src.ServiceType));
            CreateMap<CreateUpdateServiceDTO, Service>();
        }
    }
}
