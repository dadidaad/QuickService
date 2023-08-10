using AutoMapper;
using QuickServiceWebAPI.DTOs.Attachment;
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

        public List<RequestTicketDTO> GetQueryRequestTicket(string? assignee, DateTime? createFrom, DateTime? createTo, string? description,
                                                         string? group, string? requester, string? requestType, string? priority, string? status)
        {
            var requestTickets = _repository.GetQueryRequestTicket(assignee, createFrom, createTo, description, group, requester, requestType, priority, status);
            return requestTickets.Select(requestTicket => _mapper.Map<RequestTicketDTO>(requestTicket)).ToList();
        }
    }
}
