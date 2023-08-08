namespace QuickServiceWebAPI.Services
{
    public interface IDashboardService
    {
        public Task<int> GetRequestTicketCount();
        public Task<int> GetRequestTicketIncidentCount();
        public Task<int> GetProblemCount();
        public Task<int> GetChangeCount();
        public Task<List<Dictionary<string, int>>> GetRequestTicketByServiceCategoryCount();
    }
}
