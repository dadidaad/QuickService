using System.Threading.Tasks;

namespace QuickServiceWebAPI.Repositories
{
    public interface IDashboardRepository
    {
        public Task<int> GetRequestTicketCount();
        public Task<int> GetRequestTicketIncidentCount();
        public Task<int> GetProblemCount();
        public Task<int> GetChangeCount();

        public Task <Dictionary<string, int>> GetRequestTicketByServiceCategoryCount();
    }
}
