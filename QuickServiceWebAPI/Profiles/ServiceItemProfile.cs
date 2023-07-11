using AutoMapper;
using QuickServiceWebAPI.DTOs.Service;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class ServiceItemProfile : Profile
    {
        public ServiceItemProfile()
        {
            CreateMap<ServiceItem, ServiceItemDTO>()
                .ForMember(dest => dest.AttachmentEntity,
                opt => opt.MapFrom(src => src.Attachment))
                .ForMember(dest => dest.ServiceCategoryEntity,
                opt => opt.MapFrom(src => src.ServiceCategory));
            CreateMap<CreateUpdateServiceItemDTO, ServiceItem>();
        }
    }
}
