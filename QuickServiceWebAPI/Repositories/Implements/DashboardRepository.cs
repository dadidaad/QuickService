using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Utilities;

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
        public async Task<Dictionary<string, int>> GetRequestTicketByProblemStatusCount()
        {
            try
            {
                var statusValues = Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>().Select(s => s.ToString());

                var query = from status in statusValues
                            join problem in _context.Problems on status equals ((StatusEnum)Enum.Parse(typeof(StatusEnum), problem.Status.ToString())).ToString() into tickets
                            from ticket in tickets.DefaultIfEmpty()
                            group ticket by status into ticketGroup
                            select new
                            {
                                Status = ticketGroup.Key,
                                TotalTickets = ticketGroup.Count(t => t != null)
                            };
                var resultDictionary = query.ToDictionary(s => s.Status, r => r.TotalTickets);
                return resultDictionary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Dictionary<string, int>> GetRequestTicketByProblemPriorityCount()
        {
            try
            {
                var priorityValues = Enum.GetValues(typeof(PriorityEnum)).Cast<PriorityEnum>().Select(s => s.ToString());

                var query = from priority in priorityValues
                            join problem in _context.Problems on priority equals ((PriorityEnum)Enum.Parse(typeof(PriorityEnum), problem.Priority.ToString())).ToString() into tickets
                            from ticket in tickets.DefaultIfEmpty()
                            group ticket by priority into ticketGroup
                            select new
                            {
                                Priority = ticketGroup.Key,
                                TotalTickets = ticketGroup.Count(t => t != null)
                            };
                var resultDictionary = query.ToDictionary(s => s.Priority, r => r.TotalTickets);
                return resultDictionary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Dictionary<string, int>> GetRequestTicketByProblemImpactCount()
        {
            try
            {
                var impactValues = Enum.GetValues(typeof(ImpactEnum)).Cast<ImpactEnum>().Select(i => i.ToString());

                var query = from impact in impactValues
                            join problem in _context.Problems on impact equals ((ImpactEnum)Enum.Parse(typeof(ImpactEnum), problem.Impact.ToString())).ToString() into tickets
                            from ticket in tickets.DefaultIfEmpty()
                            group ticket by impact into ticketGroup
                            select new
                            {
                                Impact = ticketGroup.Key,
                                TotalTickets = ticketGroup.Count(t => t != null)
                            };
                var resultDictionary = query.ToDictionary(s => s.Impact, r => r.TotalTickets);
                return resultDictionary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }


        public async Task<Dictionary<string, int>> GetRequestTicketByChangeStatusCount()
        {
            try
            {
                var statusValues = Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>().Select(s => s.ToString());

                var query = from status in statusValues
                            join change in _context.Changes on status equals ((StatusEnum)Enum.Parse(typeof(StatusEnum), change.Status.ToString())).ToString() into tickets
                            from ticket in tickets.DefaultIfEmpty()
                            group ticket by status into ticketGroup
                            select new
                            {
                                Status = ticketGroup.Key,
                                TotalTickets = ticketGroup.Count(t => t != null)
                            };
                var resultDictionary = query.ToDictionary(s => s.Status, r => r.TotalTickets);
                return resultDictionary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Dictionary<string, int>> GetRequestTicketByChangeChangeTypeCount()
        {
            try
            {
                var changeTypeValues = Enum.GetValues(typeof(ChangeTypeEnum)).Cast<ChangeTypeEnum>().Select(s => s.ToString());

                var query = from changeType in changeTypeValues
                            join change in _context.Changes on changeType equals ((ChangeTypeEnum)Enum.Parse(typeof(ChangeTypeEnum), change.ChangeType.ToString())).ToString() into tickets
                            from ticket in tickets.DefaultIfEmpty()
                            group ticket by changeType into ticketGroup
                            select new
                            {
                                ChangeType = ticketGroup.Key,
                                TotalTickets = ticketGroup.Count(t => t != null)
                            };
                var resultDictionary = query.ToDictionary(s => s.ChangeType, r => r.TotalTickets);
                return resultDictionary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Dictionary<string, int>> GetRequestTicketByChangeImpactCount()
        {
            try
            {
                var impactValues = Enum.GetValues(typeof(ImpactEnum)).Cast<ImpactEnum>().Select(i => i.ToString());

                var query = from impact in impactValues
                            join change in _context.Changes on impact equals ((ImpactEnum)Enum.Parse(typeof(ImpactEnum), change.Impact.ToString())).ToString() into tickets
                            from ticket in tickets.DefaultIfEmpty()
                            group ticket by impact into ticketGroup
                            select new
                            {
                                Impact = ticketGroup.Key,
                                TotalTickets = ticketGroup.Count(t => t != null)
                            };

                var resultDictionary = query.ToDictionary(s => s.Impact, r => r.TotalTickets);
                return resultDictionary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Dictionary<string, int>> GetRequestTicketByStatusCount()
        {
            try
            {
                var statusValues = Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>().Select(s => s.ToString());

                var query = from status in statusValues
                            join requestTicket in _context.RequestTickets on status equals ((StatusEnum)Enum.Parse(typeof(StatusEnum), requestTicket.Status.ToString())).ToString() into tickets
                            from ticket in tickets.DefaultIfEmpty()
                            group ticket by status into ticketGroup
                            select new
                            {
                                Status = ticketGroup.Key,
                                TotalTickets = ticketGroup.Count(t => t != null)
                            };
                var resultDictionary = query.ToDictionary(s => s.Status, r => r.TotalTickets);
                return resultDictionary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Dictionary<string, int>> GetRequestTicketByPriorityCount()
        {
            try
            {
                var priorityValues = Enum.GetValues(typeof(PriorityEnum)).Cast<PriorityEnum>().Select(p => p.ToString());

                var query = from priority in priorityValues
                            join requestTicket in _context.RequestTickets on
                                priority equals ((PriorityEnum)Enum.Parse(typeof(PriorityEnum), requestTicket.Priority.ToString())).ToString()
                                into tickets
                            from ticket in tickets.DefaultIfEmpty()
                            group ticket by priority into ticketGroup
                            select new
                            {
                                Priority = ticketGroup.Key,
                                TotalTickets = ticketGroup.Count(t => t != null)
                            };
                var resultDictionary = query.ToDictionary(s => s.Priority, r => r.TotalTickets);
                return resultDictionary;
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

        public async Task<List<dynamic>> CountRequestTicketByDay(DateTime fromDate, DateTime toDate)
        {
            try
            {
                IQueryable<RequestTicket> query = _context.RequestTickets;
                var groupedQuery = await query.GroupBy(q => q.CreatedAt.Date)
                    .OrderByDescending(c => c.Key)
                     .Select(grp => new
                     {
                         Date = grp.Key,
                         TotalCreated = grp.Count(),
                         TotalResolved = grp
                                .Where(g => (g.Status == StatusEnum.Resolved.ToString()
                                || g.Status == StatusEnum.Closed.ToString()) && g.State == StateEnum.Normal.ToString())
                                .Count(),
                         ServiceItemName = (string)null
                     }).Where(q => q.Date >= fromDate && q.Date <= toDate).ToListAsync<dynamic>();
                return groupedQuery;
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex, "An error occurred");
                throw;
            }
        }

        public async Task<List<dynamic>> CountRequestTicketByDayAndServiceItem(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var groupedQuery = EnumerableUtils
                    .DynamicListFromSql(_context,
                    "EXEC [QuickServices].[GetGroupedData] @fromDate, @toDate",
                    new Dictionary<string, object> { { "fromDate", fromDate }, { "toDate", toDate } });
                return groupedQuery.ToList();
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex, "An error occurred");
                throw;
            }
        }
    }
}
