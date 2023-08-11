using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IQueryRepository
    {
        public List<Query> GetQueries();
        public Task<Query> GetQueryById(string queryId);
        public Task AddQuery(Query query);
        public Task UpdateQuery(Query query);
        public Task DeleteQuery(Query query);
        public Task<Query> GetLastQuery();
        public List<RequestTicket> GetQueryRequestTicket(QueryDTO query);
    }
}
