using AutoMapper;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.RequestTicketHistory;
using QuickServiceWebAPI.DTOs.ServiceCategory;
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
            return requestTicketHistories.Select(requestTicketHistory => _mapper.Map<RequestTicketHistoryDTO>(requestTicketHistory)).ToList();
        }
    }
}
