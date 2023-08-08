using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IRequestTicketHistoryRepository
    {
        public Task<List<RequestTicketHistory>> GetRequestTicketHistories();
        public Task<RequestTicketHistory> GetRequestTicketHistoryByRequestTicketId(string requestTicketId);
        public Task AddRequestTicketHistory(RequestTicketHistory requestTicketHistory);
    }
}
