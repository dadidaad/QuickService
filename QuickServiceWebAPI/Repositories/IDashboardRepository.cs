using System.Threading.Tasks;

namespace QuickServiceWebAPI.Repositories
{
    public interface IDashboardRepository
    {
        public Task<int> GetRequestTicketCount();
        public Task<int> GetRequestTicketIncidentCount();
        public Task<int> GetProblemCount();
        public Task<int> GetChangeCount();
        public Task<Dictionary<string, int>> GetRequestTicketByProblemStatusCount();
        public Task<Dictionary<string, int>> GetRequestTicketByProblemPriorityCount();
        public Task<Dictionary<string, int>> GetRequestTicketByProblemImpactCount();
        public Task<Dictionary<string, int>> GetRequestTicketByChangeStatusCount();
        public Task<Dictionary<string, int>> GetRequestTicketByChangeChangeTypeCount();
        public Task<Dictionary<string, int>> GetRequestTicketByChangeImpactCount();
        public Task<Dictionary<string, int>> GetRequestTicketByStatusCount();
        public Task<Dictionary<string, int>> GetRequestTicketByPriorityCount();
        public Task <Dictionary<string, int>> GetRequestTicketByServiceCategoryCount();
        public Task<List<dynamic>> CountRequestTicketByDay(DateTime fromDate, DateTime toDate);
        public Task<List<dynamic>> CountRequestTicketByDayAndServiceItem(DateTime fromDate, DateTime toDate);
    }
}
