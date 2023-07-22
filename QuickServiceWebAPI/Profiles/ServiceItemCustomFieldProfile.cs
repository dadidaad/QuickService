using AutoMapper;
using QuickServiceWebAPI.DTOs.ServiceItemCustomField;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class ServiceItemCustomFieldProfile : Profile
    {
        public ServiceItemCustomFieldProfile()
        {
            CreateMap<CreateUpdateServiceItemCustomFieldDTO, ServiceItemCustomField>();
            CreateMap<ServiceItemCustomField, ServiceItemCustomFieldDTO>();
        }
    }
}
