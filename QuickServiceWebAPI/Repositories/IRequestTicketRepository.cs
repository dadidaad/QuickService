using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IRequestTicketRepository
    {
        public List<RequestTicket> GetRequestTickets();
        public List<RequestTicket> GetRequestTicketsCustom();
        public Task<RequestTicket> GetRequestTicketById(string requestTicketId);
        public Task<RequestTicket?> AddRequestTicket(RequestTicket requestTicket);
        public Task UpdateRequestTicket(RequestTicket requestTicket);
        public Task DeleteRequestTicket(RequestTicket requestTicket);
        public Task<RequestTicket> GetLastRequestTicket();
        public List<RequestTicket> GetRequestTicketsForRequester(string requester);
        public Task<List<RequestTicket>> GetAllRequestTicketRelatedToWorkflow(string workflowId);
        public Task<List<RequestTicket>> GetAllRequestTicketRelatedToServiceItem(string serviceItemId);
        public Task<List<TicketQueryAdminDTO>> GetRequestTicketsQueryAdmin(QueryDTO queryDto);
        public Task<List<TicketQueryAdminDTO>> GetRequestTicketsFilterUser(QueryConfigDTO queryDto);
        public Task DeleteRequestTicketRelatedToServiceItem(string serviceItemId);
    }
}
