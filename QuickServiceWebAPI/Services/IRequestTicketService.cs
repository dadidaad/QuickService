using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;

namespace QuickServiceWebAPI.Services
{
    public interface IRequestTicketService
    {
        public Task<RequestTicketDTO> SendRequestTicket(CreateRequestTicketDTO createRequestTicketDTO);
        public Task<List<RequestTicketDTO>> GetAllListRequestTicket();
        public Task<List<TicketQueryAdminDTO>> GetRequestTicketsAdmin(string ticketType, string queryId);
        public Task<List<TicketQueryAdminDTO>> GetRequestTicketsQueryAdmin(QueryDTO queryDto);
        public Task<List<TicketQueryAdminDTO>> GetRequestTicketsFilterUser(QueryConfigDTO queryDto);
        public Task<List<RequestTicketForRequesterDTO>> GetAllListRequestTicketForRequester(RequesterResquestDTO requesterResquestDTO);
        public Task<RequestTicketDTO> GetDetailsRequestTicket(string requestTicketId);
        public Task<RequestTicketForRequesterDTO> GetDetailsRequestTicketForRequester(RequesterResquestDTO requesterResquestDTO);
        public Task<RequestTicketDTO> UpdateRequestTicket(UpdateRequestTicketDTO updateRequestTicketDTO);
        public Task CancelRequestTicket(string requestTicketId);
        public Task ConfirmRequestTicket(string requestTicketId);
        public Task UpdateTicketStateJobAsync();
    }
}
