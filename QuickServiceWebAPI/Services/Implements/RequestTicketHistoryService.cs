using AutoMapper;
using QuickServiceWebAPI.DTOs.RequestTicketHistory;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class RequestTicketHistoryService : IRequestTicketHistoryService
    {
        private readonly IRequestTicketHistoryRepository _repository;
        private readonly IMapper _mapper;
        public RequestTicketHistoryService(IRequestTicketHistoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> GetNextIdRequestTicketHistory()
        {
            RequestTicketHistory lastRequestTicketHistory = await _repository.GetLastRequestTicketHistory();
            int id = 0;
            if (lastRequestTicketHistory == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastRequestTicketHistory.RequestTicketHistoryId) + 1;
            }
            string requestTicketHistoryId = IDGenerator.GenerateRequestTicketHistoryId(id);
            return requestTicketHistoryId;
        }

        public async Task<List<RequestTicketHistoryDTO>> GetRequestTicketHistoryByRequestTicketId(string requestTicketId)
        {
            var requestTicketHistories = await _repository.GetRequestTicketHistoryByRequestTicketId(requestTicketId);
            if (requestTicketHistories == null)
            {
                throw new AppException("Request ticket history not found");
            }
            return requestTicketHistories.Select(requestTicketHistory => _mapper.Map<RequestTicketHistoryDTO>(requestTicketHistory)).ToList();
        }

        public async Task<List<RequestTicketHistoryDTO>> GetRequestTicketHistoryByProblemId(string problemId)
        {
            var requestTicketHistories = await _repository.GetRequestTicketHistoryByProblemId(problemId);
            if (requestTicketHistories == null)
            {
                throw new AppException("Request ticket history not found");
            }
            return requestTicketHistories.Select(requestTicketHistory => _mapper.Map<RequestTicketHistoryDTO>(requestTicketHistory)).ToList();
        }

        public async Task<List<RequestTicketHistoryDTO>> GetRequestTicketHistoryByChangeId(string changeId)
        {
            var requestTicketHistories = await _repository.GetRequestTicketHistoryByChangeId(changeId);
            if (requestTicketHistories == null)
            {
                throw new AppException("Request ticket history not found");
            }
            return requestTicketHistories.Select(requestTicketHistory => _mapper.Map<RequestTicketHistoryDTO>(requestTicketHistory)).ToList();
        }
    }
}
