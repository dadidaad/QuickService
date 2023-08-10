using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;

namespace QuickServiceWebAPI.Services
{
    public interface IQueryService 
    {
        public List<RequestTicketDTO> GetQueryRequestTicket(QueryDTO query);
    }
}
