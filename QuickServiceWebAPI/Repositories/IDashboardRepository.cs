namespace QuickServiceWebAPI.Repositories
{
    public interface IDashboardRepository
    {
        public Task<int> GetRequestTicketCount();
        public Task<int> GetRequestTicketIncidentCount();
        public Task<int> GetProblemCount();
        public Task<int> GetChangeCount();

        public Task<List<int>> GetRequestTicketByServiceCategoryCount();
    }
}
