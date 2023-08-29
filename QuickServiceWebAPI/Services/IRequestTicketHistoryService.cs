using QuickServiceWebAPI.DTOs.RequestTicketHistory;

namespace QuickServiceWebAPI.Services
{
    public interface IRequestTicketHistoryService
    {
        public Task<List<RequestTicketHistoryDTO>> GetRequestTicketHistoryByRequestTicketId(string requestTicketId);

        public Task<List<RequestTicketHistoryDTO>> GetRequestTicketHistoryByChangeId(string changeId);
        public Task<List<RequestTicketHistoryDTO>> GetRequestTicketHistoryByProblemId(string problemId);

        public Task<string> GetNextIdRequestTicketHistory();
    }
}
