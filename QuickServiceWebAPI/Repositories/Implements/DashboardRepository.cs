using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly QuickServiceContext _context;
        private readonly ILogger<DashboardRepository> _logger;

        public DashboardRepository(QuickServiceContext context, ILogger<DashboardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> GetRequestTicketCount()
        {
            try
            {
                return await _context.RequestTickets.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<int> GetRequestTicketIncidentCount()
        {
            try
            {
                return await _context.RequestTickets.Where(r => r.IsIncident == true).CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<int> GetChangeCount()
        {
            try
            {
                return await _context.Changes.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<int> GetProblemCount()
        {
            try
            {
                return await _context.Problems.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<List<int>> GetRequestTicketByServiceCategoryCount()
        {
            try
            {
                var serviceCategory = await _context.ServiceCategories.Include(category => category.ServiceItems)
                                      .ThenInclude(item => item.RequestTickets).ToListAsync();

                List<int> requestTicketCounts = new List<int>();

                foreach (var item in serviceCategory)
                {
                    var requestTicketCount = item.ServiceItems
                        .SelectMany(item => item.RequestTickets)
                        .Count();

                    requestTicketCounts.Add(requestTicketCount);
                }

                return requestTicketCounts;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
    }
}
