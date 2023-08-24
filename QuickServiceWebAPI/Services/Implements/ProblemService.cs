using AutoMapper;
using QuickServiceWebAPI.DTOs.Problem;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Repositories;

namespace QuickServiceWebAPI.Services.Implements
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _repository;
        private readonly IMapper _mapper;

        public ProblemService(IProblemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public List<ProblemDTO> GetAllProblems()
        {
            var changes = _repository.GetProblems();
            return changes.Select(change => _mapper.Map<ProblemDTO>(change)).ToList();
        }

        public async Task<List<TicketQueryAdminDTO>> GetRequestTicketsQueryAdmin(QueryDTO queryDto)
        {
            return await _repository.GetRequestTicketsQueryAdmin(queryDto);
        }
    }
}
