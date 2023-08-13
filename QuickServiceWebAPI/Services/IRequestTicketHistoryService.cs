using QuickServiceWebAPI.DTOs.RequestTicketHistory;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Services
{
    public interface IRequestTicketHistoryService
    {
        public Task<List<RequestTicketHistoryDTO>> GetRequestTicketHistoryByRequestTicketId(string requestTicketId);

        public Task<string> GetNextIdRequestTicketHistory();
    }
}
