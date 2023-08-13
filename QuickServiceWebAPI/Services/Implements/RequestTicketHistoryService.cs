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

        public async Task CreateRequestTicketHistoryFirst(CreateRequestTicketHistoryDTO createRequestTicketHistoryDTO, RequestTicket requestTicket)
        {
            var requestTicketHistory = _mapper.Map<RequestTicketHistory>(createRequestTicketHistoryDTO);
            requestTicketHistory.RequestTicketHistoryId = await GetNextIdRequestTicketHistory();
            requestTicketHistory.RequestTicketId = requestTicket.RequestTicketId;
            requestTicketHistory.Content = $"{requestTicket.RequesterId} Create the request";
            requestTicketHistory.LastUpdate = requestTicket.CreatedAt;
            requestTicketHistory.UserId = requestTicket.RequesterId;
            await _repository.AddRequestTicketHistory(requestTicketHistory);
        }

        public Task CreateRequestTicketHistoryUpdate(CreateRequestTicketHistoryDTO createRequestTicketHistoryDTO)
        {
            throw new NotImplementedException();
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
    }
}
