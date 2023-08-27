using AutoMapper;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class ServiceItemProfile : Profile
    {
        public ServiceItemProfile()
        {
            CreateMap<ServiceItem, ServiceItemDTO>()
                .ForMember(dest => dest.ServiceCategoryEntity,
                opt => opt.MapFrom(src => src.ServiceCategory))
                .ForMember(dest => dest.WorkflowEntity,
                opt => opt.MapFrom(src => src.Workflow))
                .ForMember(dest => dest.SlaEntity,
                opt => opt.MapFrom(src => src.Sla));
            CreateMap<ServiceItem, ServiceItemDTOSecond>();
            CreateMap<CreateUpdateServiceItemDTO, ServiceItem>()
                .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status ? StatusServiceItemEnum.Published.ToString() 
                : StatusServiceItemEnum.Drafted.ToString()))
                .IgnoreAllNonExisting();
        }
    }
}
