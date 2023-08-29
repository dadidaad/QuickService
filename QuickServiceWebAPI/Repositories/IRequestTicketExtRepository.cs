using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IRequestTicketExtRepository
    {
        public List<RequestTicketExt> GetRequestTicketExts();
        public Task<RequestTicketExt> GetRequestTicketExtById(string requestTicketExtId);
        public Task<List<RequestTicketExt>> GetRequestTicketExtsForTicket(string requestTicketExtId);
        public Task AddRequestTicketExt(RequestTicketExt requestTicketExt);
        public Task UpdateRequestTicketExt(RequestTicketExt requestTicketExt);
        public Task DeleteRequestTicketExt(RequestTicketExt requestTicketExt);
        public Task<RequestTicketExt> GetLastRequestTicketExt();
    }
}
