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
            CreateMap<CreateUpdateServiceCategoryDTO, ServiceCategory>();
        }
    }
}
