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

        public async Task<Dictionary<string, int>> GetRequestTicketByServiceCategoryCount()
        {
            try
            {
                var query = from sc in _context.ServiceCategories
                            join si in _context.ServiceItems on sc.ServiceCategoryId equals si.ServiceCategoryId
                            join rt in _context.RequestTickets on si.ServiceItemId equals rt.ServiceItemId into rtGroup
                            from rt in rtGroup.DefaultIfEmpty()
                            group rt by sc.ServiceCategoryId into g
                            select new
                            {
                                ServiceCategoryID = g.Key,
                                RequestCount = g.Count(rt => rt != null)
                            };
                var resultDictionary = query.ToDictionary(s => s.ServiceCategoryID, r => r.RequestCount);
                return resultDictionary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
    }
}
