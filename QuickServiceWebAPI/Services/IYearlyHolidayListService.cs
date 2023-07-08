using QuickServiceWebAPI.DTOs.YearHolidayListDTO;

namespace QuickServiceWebAPI.Services
{
    public interface IYearlyHolidayListService
    {
        public List<YearlyHolidayListDTO> GetYearlyHolidayList();
        public Task CreateYearlyHoliday(CreateUpdateYearlyHolidayListDTO createUpdateYearlyHolidayListDTO);
    }
}
