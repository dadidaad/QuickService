using Azure.Core;
using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class RequestTicketRepository : IRequestTicketRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<RequestTicketRepository> _logger;
        public RequestTicketRepository(QuickServiceContext context, ILogger<RequestTicketRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<RequestTicket?> AddRequestTicket(RequestTicket requestTicket)
        {
            try
            {
                _context.RequestTickets.Add(requestTicket);
                await _context.SaveChangesAsync();
                var entry = _context.Entry(requestTicket);
                return requestTicket;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<RequestTicket> GetRequestTicketById(string requestTicketId)
        {
            try
            {
                RequestTicket requestTicket = await _context.RequestTickets.Include(g => g.AssignedToGroupNavigation)
                    .Include(u => u.AssignedToNavigation)
                    .Include(a => a.Attachment)
                    .Include(r => r.Requester)
                    .Include(s => s.ServiceItem).ThenInclude(si => si.ServiceCategory)
                    .Include(r => r.Workflow)
                    .Include(sl => sl.Sla)
                    .ThenInclude(slm => slm.Slametrics)
                    .FirstOrDefaultAsync(x => x.RequestTicketId == requestTicketId);
                return requestTicket;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<RequestTicket> GetRequestTickets()
        {
            try
            {
                return _context.RequestTickets.Include(u => u.AssignedToNavigation).Include(s => s.ServiceItem)
                                              .Include(a => a.Attachment).Include(u => u.Requester)
                                              .Include(s => s.Sla).ThenInclude(s => s.Slametrics).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateRequestTicket(RequestTicket requestTicket)
        {
            try
            {
                _context.RequestTickets.Update(requestTicket);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteRequestTicket(RequestTicket requestTicket)
        {
            try
            {
                _context.RequestTickets.Remove(requestTicket);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<RequestTicket> GetLastRequestTicket()
        {
            try
            {
                return await _context.RequestTickets.OrderByDescending(u => u.RequestTicketId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public List<RequestTicket> GetRequestTicketsForRequester(string requester)
        {
            try
            {
                return _context.RequestTickets.Include(u => u.AssignedToNavigation).Include(s => s.ServiceItem)
                                              .Include(a => a.Attachment).Include(u => u.Requester)
                                              .Include(s => s.Sla).ThenInclude(s => s.Slametrics)
                                              .Where(r => r.Requester.Email == requester).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }      
    }
}
