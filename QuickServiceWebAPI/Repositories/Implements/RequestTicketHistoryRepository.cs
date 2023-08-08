using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class RequestTicketHistoryRepository : IRequestTicketHistoryRepository
    {
        private readonly QuickServiceContext _context;
        private readonly ILogger<RequestTicketHistoryRepository> _logger;

        public RequestTicketHistoryRepository(QuickServiceContext context, ILogger<RequestTicketHistoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddRequestTicketHistory(RequestTicketHistory requestTicketHistory)
        {
            try
            {
                _context.RequestTicketHistories.Add(requestTicketHistory);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<RequestTicketHistory> GetRequestTicketHistoryByRequestTicketId(string requestTicketId)
        {
            try
            {
                return await _context.RequestTicketHistories.Include(r => r.RequestTicket).Include(u => u.User)
                    .AsNoTracking().FirstOrDefaultAsync(r => r.RequestTicketId == requestTicketId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<List<RequestTicketHistory>> GetRequestTicketHistories()
        {
            try
            {
                return await _context.RequestTicketHistories.Include(r => r.RequestTicket).Include(u => u.User).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
    }
}
