using QuickServiceWebAPI.DTOs.RequestTicketHistory;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Services
{
    public interface IRequestTicketHistoryService
    {
        public Task CreateRequestTicketHistoryFirst(CreateRequestTicketHistoryDTO createRequestTicketHistoryDTO, RequestTicket requestTicket);

        public Task CreateRequestTicketHistoryUpdate(CreateRequestTicketHistoryDTO createRequestTicketHistoryDTO);

        public Task<string> GetNextIdRequestTicketHistory();
    }
}
