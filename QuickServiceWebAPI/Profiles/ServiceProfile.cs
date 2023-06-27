using AutoMapper;
using QuickServiceWebAPI.DTOs.Service;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceDTO>();
            CreateMap<CreateUpdateServiceDTO, Service>();
        }
    }
}
