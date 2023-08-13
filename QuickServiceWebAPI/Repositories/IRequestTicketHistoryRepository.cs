using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IRequestTicketHistoryRepository
    {
        public Task<List<RequestTicketHistory>> GetRequestTicketHistories();
        public Task<List<RequestTicketHistory>> GetRequestTicketHistoryByRequestTicketId(string requestTicketId);
        public Task AddRequestTicketHistory(RequestTicketHistory requestTicketHistory);

        public Task<RequestTicketHistory> GetLastRequestTicketHistory();
    }
}
