using AutoMapper;
using QuickServiceWebAPI.DTOs.ServiceType;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class ServiceTypeProfile : Profile
    {
        public ServiceTypeProfile() {
            CreateMap<ServiceType, ServiceTypeDTO>();
            CreateMap<CreateUpdateServiceTypeDTO, ServiceType>().IgnoreAllNonExisting();
        }
    }
}
