using AutoMapper;
using QuickServiceWebAPI.DTOs.BusinessHour;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class BusinessHourProfile : Profile
    {
        public BusinessHourProfile()
        {
            CreateMap<BusinessHour, BusinessHourDTO>();
            CreateMap<CreateUpdateBusinessHourDTO, BusinessHour>();
        }
    }
}
