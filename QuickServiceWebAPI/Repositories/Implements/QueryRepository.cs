using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

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
