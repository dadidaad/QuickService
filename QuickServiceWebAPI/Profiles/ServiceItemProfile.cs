using AutoMapper;
using QuickServiceWebAPI.DTOs.Service;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;
using System.Security.Cryptography;

namespace QuickServiceWebAPI.Profiles
{
    public class ServiceItemProfile : Profile
    {
        public ServiceItemProfile()
        {
            CreateMap<ServiceItem, ServiceItemDTO>()
                .ForMember(dest => dest.ServiceCategoryEntity,
                opt => opt.MapFrom(src => src.ServiceCategory));
            CreateMap<CreateUpdateServiceItemDTO, ServiceItem>()
                .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status ? "Published" : "Draft")).IgnoreAllNonExisting();
        }
    }
}
