using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class QueryRepository : IQueryRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<QueryRepository> _logger;
        public QueryRepository(QuickServiceContext context, ILogger<QueryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddQuery(Query query)
        {
            try
            {
                _context.Queries.Add(query);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Query> GetQueryById(string queryId)
        {
            try
            {
                Query query = await _context.Queries.Include(u => u.User).AsNoTracking().FirstOrDefaultAsync(x => x.QueryId == queryId);
                return query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<RequestTicket> GetQueryRequestTicket(QueryDTO query)
        {
            try
            {

                return _context.RequestTickets.Include(g => g.AssignedToGroupNavigation)
                    .Include(u => u.AssignedToNavigation)
                    .Include(a => a.Attachment)
                    .Include(r => r.Requester)
                    .Include(s => s.ServiceItem)
                    .Include(sl => sl.Sla)
                    .ThenInclude(slm => slm.Slametrics)
                    .Where(x => (query.Assignee.Contains(x.AssignedToNavigation.FirstName + x.AssignedToNavigation.LastName) || query.Assignee == null) &&
                        ((query.CreateFrom == null || x.CreatedAt >= query.CreateFrom)  && (query.CreateTo == null || x.CreatedAt <= query.CreateTo)) &&
                        (query.Description == null || x.Description.Contains(query.Description)) &&
                        (query.Group == null || query.Group.Contains(x.AssignedToGroupNavigation.GroupName)) &&
                        (query.Reporter == null || query.Reporter.Contains(x.Requester.FirstName + " " + x.Requester.LastName)) &&
                        (query.Service == null || query.Service.Contains(x.ServiceItem.ServiceCategory.ServiceCategoryName)) &&
                        (query.Priority.Contains(x.Priority) || query.Priority == null) &&
                        (query.RequestType == null || query.RequestType.Contains(x.ServiceItem.ServiceItemName)) &&
                        (query.Status.Contains(x.Status) || query.Status == null))
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<Query> GetQueries()
        {
            try
            {
                return _context.Queries.Include(u => u.User).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateQuery(Query query)
        {
            try
            {
                _context.Queries.Update(query);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteQuery(Query query)
        {
            try
            {
                _context.Queries.Remove(query);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Query> GetLastQuery()
        {
            try
            {
                return await _context.Queries.OrderByDescending(u => u.QueryId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
