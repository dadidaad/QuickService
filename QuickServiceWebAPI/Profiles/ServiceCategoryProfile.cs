using AutoMapper;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class ServiceCategoryProfile : Profile
    {
        public ServiceCategoryProfile()
        {
            CreateMap<ServiceCategory, ServiceCategoryDTO>();
            CreateMap<ServiceCategory, ServiceCategoryWithServiceItemDTO>()
                .ForMember(dest => dest.ServiceItemEntities,
                opt => opt.MapFrom(src => src.ServiceItems));
            CreateMap<CreateUpdateServiceCategoryDTO, ServiceCategory>();
        }
    }
}
