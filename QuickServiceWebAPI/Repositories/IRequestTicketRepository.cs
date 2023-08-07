using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IRequestTicketRepository
    {
        public List<RequestTicket> GetRequestTickets();
        public Task<RequestTicket> GetRequestTicketById(string requestTicketId);
        public Task<RequestTicket?> AddRequestTicket(RequestTicket requestTicket);
        public Task UpdateRequestTicket(RequestTicket requestTicket);
        public Task DeleteRequestTicket(RequestTicket requestTicket);
        public Task<RequestTicket> GetLastRequestTicket();
        public List<RequestTicket> GetRequestTicketsForRequester(string requester);
    }
}
