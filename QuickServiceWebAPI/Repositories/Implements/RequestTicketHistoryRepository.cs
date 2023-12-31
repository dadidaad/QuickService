﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<List<RequestTicketHistory>> GetRequestTicketHistoryByRequestTicketId(string requestTicketId)
        {
            try
            {
                return await _context.RequestTicketHistories.Include(r => r.RequestTicket).Include(u => u.User)
                    .AsNoTracking().Where(r => r.RequestTicketId == requestTicketId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<List<RequestTicketHistory>> GetRequestTicketHistoryByChangeId(string changeId)
        {
            try
            {
                return await _context.RequestTicketHistories.Include(r => r.Change).Include(u => u.User)
                    .AsNoTracking().Where(r => r.ChangeId == changeId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<List<RequestTicketHistory>> GetRequestTicketHistoryByProblemId(string problemId)
        {
            try
            {
                return await _context.RequestTicketHistories.Include(r => r.Problem).Include(u => u.User)
                    .AsNoTracking().Where(r => r.ProblemId == problemId).ToListAsync();
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
                return await _context.RequestTicketHistories.Include(r => r.RequestTicket)
                    .Include(c => c.Change).Include(p => p.Problem).Include(u => u.User).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<RequestTicketHistory> GetLastRequestTicketHistory()
        {
            try
            {
                return await _context.RequestTicketHistories.OrderByDescending(u => u.RequestTicketHistoryId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
