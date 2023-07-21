using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class RequestTicketExtRepository : IRequestTicketExtRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<RequestTicketExtRepository> _logger;
        public RequestTicketExtRepository(QuickServiceContext context, ILogger<RequestTicketExtRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddRequestTicketExt(RequestTicketExt requestTicketExt)
        {
            try
            {
                _context.RequestTicketExts.Add(requestTicketExt);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<RequestTicketExt> GetRequestTicketExtById(string requestTicketExtId)
        {
            try
            {
                RequestTicketExt requestTicketExt = await _context.RequestTicketExts.Include(r => r.Ticket).Include(f => f.Field)
                                                    .FirstOrDefaultAsync(x => x.RequestTicketExId == requestTicketExtId);
                return requestTicketExt;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<RequestTicketExt> GetRequestTicketExts()
        {
            try
            {
                return _context.RequestTicketExts.Include(r => r.Ticket).Include(f => f.Field).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateRequestTicketExt(RequestTicketExt requestTicketExt)
        {
            try
            {
                _context.RequestTicketExts.Update(requestTicketExt);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteRequestTicketExt(RequestTicketExt requestTicketExt)
        {
            try
            {
                _context.RequestTicketExts.Remove(requestTicketExt);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<RequestTicketExt> GetLastRequestTicketExt()
        {
            try
            {
                return await _context.RequestTicketExts.OrderByDescending(u => u.RequestTicketExId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
