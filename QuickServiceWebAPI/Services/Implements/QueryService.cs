using AutoMapper;
using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Repositories;

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

        public List<RequestTicketDTO> GetQueryRequestTicket(QueryDTO query)
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
    }
}
