using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly QuickServiceContext _context;
        private readonly ILogger<ProblemRepository> _logger;

        public ProblemRepository(QuickServiceContext context, ILogger<ProblemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddProblem(Problem problem)
        {
            try
            {
                _context.Problems.Add(problem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Problem> GetProblemById(string problemId)
        {
            try
            {
                return await _context.Problems.Include(c => c.Assignee)
                    .Include(c => c.Sla).ThenInclude(s => s.Slametrics)
                    .Include(c => c.Attachment)
                    .Include(c => c.Attachment)
                    .Include(u => u.Requester)
                    .Include(r => r.RequestTickets)
                    .FirstOrDefaultAsync(x => x.ProblemId == problemId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<Problem> GetProblems()
        {
            try
            {
                return _context.Problems.Include(c => c.Assignee)
                    .Include(c => c.Sla).ThenInclude(s => s.Slametrics)
                    .Include(c => c.Attachment)
                    .Include(u => u.Requester)
                    .Include(r => r.RequestTickets)
                    .Include(c => c.Attachment).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateProblem(Problem problem)
        {
            try
            {
                _context.Problems.Update(problem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteProblem(Problem problem)
        {
            try
            {
                _context.Problems.Remove(problem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Problem> GetLastProblem()
        {
            try
            {
                return await _context.Problems.OrderByDescending(u => u.ProblemId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public Task<List<TicketQueryAdminDTO>> GetRequestTicketsQueryAdmin(QueryDTO queryDto)
        {
            var listTickets = new List<Problem>();
            var hasQueryConfig = !string.IsNullOrEmpty(queryDto.QueryStatement);
            if (!hasQueryConfig) listTickets = GetProblems();
            var queryConfig = new QueryConfigDTO();
            if (hasQueryConfig) queryConfig = JsonConvert.DeserializeObject<QueryConfigDTO>(queryDto.QueryStatement);
            var listTicketsDto = new List<TicketQueryAdminDTO>();

            if (queryConfig == null) return Task.FromResult(listTicketsDto);

            if (hasQueryConfig) listTickets = _context.Problems
                     .Include(u => u.Assignee)
                     .Include(a => a.Attachment)
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
                        TicketId = q.ProblemId,
                        Title = q.Title,
                        GroupId = q.Group != null ? q.Group.GroupId : null,
                        GroupName = q.Group != null ? q.Group.GroupName : null,
                        AssigneeId = q.AssigneeId,
                        AssigneeFullName = q.Assignee != null ? $"{q.Assignee.FirstName} {q.Assignee.MiddleName} {q.Assignee.LastName}" : null,
                        Status = q.Status,
                        CreatedAt = q.CreatedTime,
                        Priority = q.Priority,
                        Type = "problem"
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
