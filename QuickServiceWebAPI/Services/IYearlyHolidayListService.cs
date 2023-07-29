using QuickServiceWebAPI.DTOs.YearHolidayList;

namespace QuickServiceWebAPI.Services
{
    public interface IYearlyHolidayListService
    {
        public List<YearlyHolidayListDTO> GetYearlyHolidayList();
        public Task CreateYearlyHoliday(CreateUpdateYearlyHolidayListDTO createUpdateYearlyHolidayListDTO);
    }
}
