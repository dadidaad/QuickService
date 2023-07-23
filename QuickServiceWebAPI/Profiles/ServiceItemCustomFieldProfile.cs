using AutoMapper;
using QuickServiceWebAPI.DTOs.ServiceItemCustomField;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class ServiceItemCustomFieldProfile : Profile
    {
        public ServiceItemCustomFieldProfile()
        {
            CreateMap<CreateUpdateServiceItemCustomFieldDTO, ServiceItemCustomField>().IgnoreAllNonExisting();
            CreateMap<ServiceItemCustomField, ServiceItemCustomFieldDTO>();
        }
    }
}
