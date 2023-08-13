using QuickServiceWebAPI.DTOs.RequestTicket;

namespace QuickServiceWebAPI.Services
{
    public interface IRequestTicketService
    {
        public Task<RequestTicketDTO> SendRequestTicket(CreateRequestTicketDTO createRequestTicketDTO);
        public Task<List<RequestTicketDTO>> GetAllListRequestTicket();
        public Task<List<RequestTicketAdminDTO>> GetRequestTicketsAdmin(string ticketType, string queryId);
        public Task<List<RequestTicketForRequesterDTO>> GetAllListRequestTicketForRequester(RequesterResquestDTO requesterResquestDTO);
        public Task<RequestTicketDTO> GetDetailsRequestTicket(string requestTicketId);
        public Task<RequestTicketForRequesterDTO> GetDetailsRequestTicketForRequester(RequesterResquestDTO requesterResquestDTO);
        public Task UpdateRequestTicket(UpdateRequestTicketDTO updateRequestTicketDTO);
        public Task DeleteRequestTicket(string requestTicketId);
    }
}
