using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IYearlyHolidayListRepository
    {
        public List<YearlyHolidayList> GetYearLyHolidayList();
        public Task AddYearlyHoliday(YearlyHolidayList yearlyHolidayList);
    }
}
