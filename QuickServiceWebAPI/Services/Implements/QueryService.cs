using AutoMapper;
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
    }
}
