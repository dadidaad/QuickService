using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;

namespace QuickServiceWebAPI.Services
{
    public interface IQueryService 
    {
        public Task<List<RequestTicketDTO>> GetQueryRequestTicket(QueryConfigDTO query);
        public Task<List<QueryDTO>> GetQueryForUser(string userId);
        public Task<QueryDTO> CreateQuery(QueryDTO queryDTO);
        public Task UpdateQuery(QueryDTO queryDTO);
        public Task DeleteQuery(string queryId);
        public Task<string> GetNextId();
    }
}
