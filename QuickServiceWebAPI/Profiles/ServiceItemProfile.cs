using AutoMapper;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class ServiceItemProfile : Profile
    {
        public ServiceItemProfile()
        {
            CreateMap<ServiceItem, ServiceItemDTO>()
                .ForMember(dest => dest.ServiceCategoryEntity,
                opt => opt.MapFrom(src => src.ServiceCategory));
            CreateMap<ServiceItem, ServiceItemDTOSecond>();
            CreateMap<CreateUpdateServiceItemDTO, ServiceItem>()
                .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status ? "Published" : "Draft")).IgnoreAllNonExisting();
        }
    }
}
