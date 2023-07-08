using AutoMapper;
using QuickServiceWebAPI.DTOs.YearHolidayListDTO;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class YearlyHolidayListProfile : Profile
    {
        public YearlyHolidayListProfile()
        {
            CreateMap<YearlyHolidayList, YearlyHolidayListDTO>()
                .ForMember(dest => dest.BusinessHourEntity,
                opt => opt.MapFrom(src => src.BusinessHour));
            CreateMap<CreateUpdateYearlyHolidayListDTO, YearlyHolidayList>();
        }
    }
}
