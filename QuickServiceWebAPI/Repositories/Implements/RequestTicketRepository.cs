using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;
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
                RequestTicket requestTicket = await _context.RequestTickets
                    .Include(g => g.AssignedToGroupNavigation)
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
                return _context.RequestTickets.Include(g => g.AssignedToGroupNavigation)
                    .Include(u => u.AssignedToNavigation)
                    .Include(a => a.Attachment)
                    .Include(r => r.Requester)
                    .Include(s => s.ServiceItem).ThenInclude(sc => sc.ServiceCategory)
                    .Include(sl => sl.Sla)
                    .ThenInclude(slm => slm.Slametrics).Take(1000).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
        public List<RequestTicket> GetRequestTicketsCustom()
        {
            try
            {
                return _context.RequestTickets.Include(u => u.AssignedToNavigation).Include(s => s.ServiceItem).ThenInclude(se => se.ServiceCategory)
                                              .Include(a => a.Attachment).Include(u => u.Requester)
                                              .Include(s => s.Sla).ThenInclude(s => s.Slametrics).Take(1000).ToList();
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

        public Task<List<TicketQueryAdminDTO>> GetRequestTicketsQueryAdmin(QueryDTO queryDto)
        {
            var listTickets = new List<RequestTicket>();
            var hasQueryConfig = !string.IsNullOrEmpty(queryDto.QueryStatement);
            if (!hasQueryConfig) listTickets = GetRequestTickets().Where(x => x.IsIncident == (queryDto.QueryType == "incident")).ToList();
            var queryConfig = new QueryConfigDTO();
            if (hasQueryConfig) queryConfig = JsonConvert.DeserializeObject<QueryConfigDTO>(queryDto.QueryStatement);
            var listTicketsDto = new List<TicketQueryAdminDTO>();

            if (queryConfig == null) return Task.FromResult(listTicketsDto);

            if (hasQueryConfig) listTickets = _context.RequestTickets.Include(g => g.AssignedToGroupNavigation)
                    .Include(u => u.AssignedToNavigation)
                    .Include(a => a.Attachment)
                    .Include(r => r.Requester)
                    .Include(s => s.ServiceItem).ThenInclude(sc => sc.ServiceCategory)
                    .Include(sl => sl.Sla)
                    .ThenInclude(slm => slm.Slametrics)
                    .Where(x =>
                        (queryConfig.Priority == null || queryConfig.Priority.Length == 0 || queryConfig.Priority.Contains(x.Priority)) &&
                        (queryConfig.TitleSearch == null || (x.Title != null && x.Title.Contains(queryConfig.TitleSearch))) &&
                        (queryConfig.Assignee == null || queryConfig.Assignee.Length == 0 || (queryConfig.Assignee != null && x.AssignedToNavigation != null && queryConfig.Assignee.Contains(x.AssignedToNavigation.FirstName + x.AssignedToNavigation.LastName))) &&
                        (queryConfig.CreatedFrom == null || x.CreatedAt >= queryConfig.CreatedFrom) &&
                        (queryConfig.CreatedTo == null || x.CreatedAt <= queryConfig.CreatedTo) &&
                        (queryConfig.Group == null || queryConfig.Group.Length == 0 || (x.AssignedToGroupNavigation != null && queryConfig.Group.Contains(x.AssignedToGroupNavigation.GroupId))) &&
                        (queryConfig.Reporter == null || queryConfig.Reporter.Length == 0 || queryConfig.Reporter.Contains(x.Requester.FirstName + " " + x.Requester.LastName)) &&
                        (queryConfig.Service == null || queryConfig.Service.Length == 0 || (x.ServiceItem != null && queryConfig.Service.Contains(x.ServiceItem.ServiceCategoryId))) &&
                        (queryConfig.RequestType == null || queryConfig.RequestType.Length == 0 || (x.ServiceItem != null && queryConfig.RequestType.Contains(x.ServiceItem.ServiceItemId))) &&
                        (queryConfig.Status == null || queryConfig.Status.Length == 0 || queryConfig.Status.Contains(x.Status))
                       ).Take(1000).ToList();

            listTicketsDto = listTickets
                    .Select(q => new TicketQueryAdminDTO()
                    {
                        TicketId = q.RequestTicketId,
                        Title = q.Title,
                        IsIncident = q.IsIncident,
                        ServiceCategoryId = q.ServiceItem != null ? q.ServiceItem.ServiceCategory.ServiceCategoryId : null,
                        ServiceCategoryName = q.ServiceItem != null ? q.ServiceItem.ServiceCategory.ServiceCategoryName : null,
                        ServiceItemId = q.ServiceItem != null ? q.ServiceItem.ServiceItemId : null,
                        ServiceItemName = q.ServiceItem != null ? q.ServiceItem.ServiceItemName : null,
                        GroupId = q.AssignedToGroupNavigation != null ? q.AssignedToGroupNavigation.GroupId : null,
                        GroupName = q.AssignedToGroupNavigation != null ? q.AssignedToGroupNavigation.GroupName : null,
                        RequesterId = q.RequesterId,
                        RequesterFullName = q.Requester.FirstName + q.Requester.MiddleName + q.Requester.LastName,
                        RequesterAvatar = q.Requester.Avatar,
                        AssigneeId = q.AssignedTo,
                        AssigneeFullName = q.AssignedToNavigation != null ? $"{q.AssignedToNavigation.FirstName} {q.AssignedToNavigation.MiddleName} {q.AssignedToNavigation.LastName}" : null,
                        AssigneeAvatar = q.AssignedToNavigation != null ? q.AssignedToNavigation.Avatar : null,
                        Status = q.Status,
                        CreatedAt = q.CreatedAt,
                        Priority = q.Priority,
                        Type = queryDto.QueryType
                    }).OrderByDescending(x=>x.CreatedAt).ToList();

            if (hasQueryConfig && queryConfig.OrderASC == true && queryConfig.OrderyBy != null)
            {
                listTicketsDto.OrderBy(x => x.GetType().GetProperty(queryConfig.OrderyBy).GetValue(x, null));
                return Task.FromResult(listTicketsDto);
            }
            if (queryConfig.OrderyBy != null) listTicketsDto.OrderByDescending(x => x.GetType().GetProperty(queryConfig.OrderyBy).GetValue(x, null));
            return Task.FromResult(listTicketsDto);
        }

        public Task<List<TicketQueryAdminDTO>> GetRequestTicketsFilterUser(QueryConfigDTO queryDto)
        {
            var listTicketsDto = new List<TicketQueryAdminDTO>();
            var listTickets = _context.RequestTickets.Include(g => g.AssignedToGroupNavigation)
                    .Include(u => u.AssignedToNavigation)
                    .Include(a => a.Attachment)
                    .Include(r => r.Requester)
                    .Include(s => s.ServiceItem).ThenInclude(sc => sc.ServiceCategory)
                    .Include(sl => sl.Sla)
                    .ThenInclude(slm => slm.Slametrics)
                    .Where(x =>
                        x.IsIncident == queryDto.IsIncident &&
                        (queryDto.Reporter == null || queryDto.Reporter.Length == 0 || queryDto.Reporter.Contains(x.RequesterId)) &&
                        (string.IsNullOrEmpty(queryDto.TitleSearch) || (x.Title != null && x.Title.Contains(queryDto.TitleSearch))) &&
                        (queryDto.CreatedFrom == null || x.CreatedAt >= queryDto.CreatedFrom) &&
                        (queryDto.CreatedTo == null || x.CreatedAt <= queryDto.CreatedTo)
                        && (queryDto.Service == null || queryDto.Service.Length == 0 || (x.ServiceItem != null && queryDto.Service.Contains(x.ServiceItem.ServiceCategoryId)))
                        && (queryDto.RequestType == null || queryDto.RequestType.Length == 0 || (x.ServiceItem != null && queryDto.RequestType.Contains(x.ServiceItem.ServiceItemId)))
                        && (queryDto.Status == null || queryDto.Status.Length == 0 || queryDto.Status.Contains(x.Status))
                       ).ToList();

            listTicketsDto = listTickets.Select(q => new TicketQueryAdminDTO()
            {
                TicketId = q.RequestTicketId,
                Title = q.Title,
                IsIncident = q.IsIncident,
                ServiceCategoryId = q.ServiceItem != null ? q.ServiceItem.ServiceCategory.ServiceCategoryId : null,
                ServiceCategoryName = q.ServiceItem != null ? q.ServiceItem.ServiceCategory.ServiceCategoryName : null,
                ServiceItemId = q.ServiceItem != null ? q.ServiceItem.ServiceItemId : null,
                ServiceItemName = q.ServiceItem != null ? q.ServiceItem.ServiceItemName : null,
                Status = q.Status,
                CreatedAt = q.CreatedAt,
            }).Take(1000).ToList();
            return Task.FromResult(listTicketsDto);
        }

        public async Task<List<RequestTicket>> GetAllRequestTicketRelatedToWorkflow(string workflowId)
        {
            try
            {
                return await _context.RequestTickets.Include(r => r.ServiceItem)
                    .Where(r => r.ServiceItem != null && r.ServiceItem.WorkflowId == workflowId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<RequestTicket>> GetAllRequestTicketRelatedToServiceItem(string serviceItemId)
        {
            try
            {
                return await _context.RequestTickets.Include(r => r.ServiceItem)
                    .Where(r => r.ServiceItem != null && r.ServiceItem.ServiceItemId == serviceItemId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteRequestTicketRelatedToServiceItem(string serviceItemId)
        {
            try
            {
                _context.RequestTickets.RemoveRange(_context.RequestTickets.Where(r => r.ServiceItemId == serviceItemId));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
