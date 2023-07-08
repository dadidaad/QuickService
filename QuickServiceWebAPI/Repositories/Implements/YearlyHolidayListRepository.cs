using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class YearlyHolidayListRepository : IYearlyHolidayListRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<YearlyHolidayListRepository> _logger;
        public YearlyHolidayListRepository(QuickServiceContext context, ILogger<YearlyHolidayListRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddYearlyHoliday(YearlyHolidayList yearlyHolidayList)
        {
            try
            {
                _context.YearlyHolidayLists.Add(yearlyHolidayList);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<YearlyHolidayList> GetYearLyHolidayList()
        {
            try
            {
                return _context.YearlyHolidayLists.Include(x => x.BusinessHour).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
    }
}
