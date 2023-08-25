using AutoMapper;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class QueryService : IQueryService
    {
        private readonly IQueryRepository _repository;
        private readonly IMapper _mapper;

        public QueryService(IQueryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<QueryDTO>> GetQueryForUser(string userId, string type)
        {
            var queries = _repository.GetQueriesForUser(userId, type);
            return await Task.FromResult(queries.Select(q => _mapper.Map<QueryDTO>(q)).ToList());
        }
        public async Task<QueryDTO> GetQueryById(string Id)
        {
            var query = await _repository.GetQueryById(Id);
            return await Task.FromResult(_mapper.Map<QueryDTO>(query));
        }
        public async Task<QueryDTO> CreateQuery(QueryDTO queryDTO)
        {
            try
            {
                var query = _mapper.Map<Query>(queryDTO);
                query.QueryId = await GetNextId();
                await _repository.AddQuery(query);

                var createQueryDTO = await _repository.GetQueryById(query.QueryId);
                return _mapper.Map<QueryDTO>(createQueryDTO); ;
            }
            catch (Exception e)
            {
                throw new AppException(e.Message);
            }

        }
        public async Task<QueryDTO> UpdateQuery(QueryDTO queryDTO)
        {
            try
            {
                Query queryCheck = await _repository.GetQueryById(queryDTO.QueryId);
                if (queryCheck == null)
                {
                    throw new AppException("Query not found");
                }
                var query = _mapper.Map<Query>(queryDTO);
                await _repository.UpdateQuery(query);

                var updateQueryDTO = await _repository.GetQueryById(query.QueryId);
                return _mapper.Map<QueryDTO>(updateQueryDTO); ;
            }
            catch (Exception e)
            {
                throw new AppException(e.Message);
            }
        }
        public Task DeleteQuery(string queryId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetNextId()
        {
            Query lastQuery = await _repository.GetLastQuery();
            int id = 0;
            if (lastQuery == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastQuery.QueryId) + 1;
            }
            string queryId = IDGenerator.GenerateQueryId(id);
            return queryId;
        }


        public List<RequestTicketDTO> GetQueryRequestTicket(QueryConfigDTO query)
        {
            var requestTickets = _repository.GetQueryRequestTicket(query);
            if (query.OrderASC == true)
            {
                requestTickets.OrderBy(x => x.GetType().GetProperty(query.OrderyBy).GetValue(x, null));
                return requestTickets.Select(requestTicket => _mapper.Map<RequestTicketDTO>(requestTicket)).ToList();
            }
            requestTickets.OrderByDescending(x => x.GetType().GetProperty(query.OrderyBy).GetValue(x, null));
            return requestTickets.Select(requestTicket => _mapper.Map<RequestTicketDTO>(requestTicket)).ToList();
        }



        Task<List<RequestTicketDTO>> IQueryService.GetQueryRequestTicket(QueryConfigDTO query)
        {
            throw new NotImplementedException();
        }


    }
}
