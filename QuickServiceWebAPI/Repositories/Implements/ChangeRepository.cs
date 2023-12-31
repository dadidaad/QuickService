﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class ChangeRepository : IChangeRepository
    {
        private readonly QuickServiceContext _context;
        private readonly ILogger<ChangeRepository> _logger;

        public ChangeRepository(QuickServiceContext context, ILogger<ChangeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddChange(Change change)
        {
            try
            {
                _context.Changes.Add(change);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Change> GetChangeById(string changeId)
        {
            try
            {
                return await _context.Changes
                    .Include(u => u.Assignee)
                    .Include(u => u.Attachment)
                    .Include(u => u.Sla).ThenInclude(s => s.Slametrics)
                    .Include(u => u.Requester)
                    .Include(r => r.RequestTickets)
                    .FirstOrDefaultAsync(c => c.ChangeId == changeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<List<Change>> GetChanges()
        {
            try
            {
                return await _context.Changes.Include(u => u.Assignee)
                    .Include(u => u.Attachment)
                    .Include(u => u.Sla).ThenInclude(s => s.Slametrics)
                    .Include(u => u.Requester)
                    .Include(r => r.RequestTickets).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateChange(Change change)
        {
            try
            {
                _context.Changes.Update(change);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteChange(Change change)
        {
            try
            {
                _context.Changes.Remove(change);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Change> GetLastChange()
        {
            try
            {
                return await _context.Changes.OrderByDescending(u => u.ChangeId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public Task<List<TicketQueryAdminDTO>> GetRequestTicketsQueryAdmin(QueryDTO queryDto)
        {
            var listTickets = new List<Change>();
            var hasQueryConfig = !string.IsNullOrEmpty(queryDto.QueryStatement);
            if (!hasQueryConfig) listTickets = GetChanges().GetAwaiter().GetResult();
            var queryConfig = new QueryConfigDTO();
            if (hasQueryConfig) queryConfig = JsonConvert.DeserializeObject<QueryConfigDTO>(queryDto.QueryStatement);
            var listTicketsDto = new List<TicketQueryAdminDTO>();

            if (queryConfig == null) return Task.FromResult(listTicketsDto);

            if (hasQueryConfig) listTickets = _context.Changes
                     .Include(a => a.Attachment)
                     //.Include(r => r.Requester)
                     .Include(c => c.Assignee)
                     .Include(g => g.Group)
                     .Where(x =>
                         (queryConfig.Priority == null || queryConfig.Priority.Length == 0 || queryConfig.Priority.Contains(x.Priority)) &&
                         (queryConfig.TitleSearch == null || (x.Title != null && x.Title.Contains(queryConfig.TitleSearch))) &&
                         (queryConfig.Assignee == null || queryConfig.Assignee.Length == 0 || (queryConfig.Assignee != null && x.Assignee != null && queryConfig.Assignee.Contains(x.Assignee.FirstName + x.Assignee.LastName))) &&
                         (queryConfig.CreatedFrom == null || x.CreatedTime >= queryConfig.CreatedFrom) &&
                         (queryConfig.CreatedTo == null || x.CreatedTime <= queryConfig.CreatedTo) &&
                         (queryConfig.Group == null || queryConfig.Group.Length == 0 || (x.Group != null && queryConfig.Group.Contains(x.Group.GroupName))) &&
                         //(queryConfig.Reporter == null || queryConfig.Reporter.Length == 0 || queryConfig.Reporter.Contains(x.Requester.FirstName + " " + x.Requester.LastName)) &&
                         (queryConfig.Status == null || queryConfig.Status.Length == 0 || queryConfig.Status.Contains(x.Status))
                        ).Take(1000).ToList();

            listTicketsDto = listTickets
                    .Select(q => new TicketQueryAdminDTO()
                    {
                        TicketId = q.ChangeId,
                        Title = q.Title,
                        //RequesterId = q.RequesterId,
                        //RequesterFullName = q.Requester.FirstName + q.Requester.MiddleName + q.Requester.LastName,
                        AssigneeId = q.AssigneeId,
                        AssigneeFullName = q.Assignee != null ? $"{q.Assignee.FirstName} {q.Assignee.MiddleName} {q.Assignee.LastName}" : null,
                        Status = q.Status,
                        CreatedAt = q.CreatedTime,
                        Priority = q.Priority,
                        Type = "change"
                    }).ToList();

            if (hasQueryConfig && queryConfig.OrderASC == true && queryConfig.OrderyBy != null)
            {
                listTicketsDto.OrderBy(x => x.GetType().GetProperty(queryConfig.OrderyBy).GetValue(x, null));
                return Task.FromResult(listTicketsDto);
            }
            if (queryConfig.OrderyBy != null) listTicketsDto.OrderByDescending(x => x.GetType().GetProperty(queryConfig.OrderyBy).GetValue(x, null));
            return Task.FromResult(listTicketsDto);
        }
    }
}
