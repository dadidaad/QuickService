using AutoMapper;
using QuickServiceWebAPI.DTOs.RequestTicketExt;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class RequestTicketExtService : IRequestTicketExtService
    {
        private readonly IRequestTicketExtRepository _repository;
        private readonly ICustomFieldRepository _customFieldRepository;
        private readonly IRequestTicketRepository _requestTicketRepository;
        private readonly IMapper _mapper;
        public RequestTicketExtService(IRequestTicketExtRepository repository, IMapper mapper, ICustomFieldRepository customFieldRepository, IRequestTicketRepository requestTicketRepository)
        {
            _requestTicketRepository = requestTicketRepository;
            _customFieldRepository = customFieldRepository;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateRequestTicketExt(List<CreateUpdateRequestTicketExtDTO> createUpdateRequestTicketExtDTOs)
        {
            var ticketId = createUpdateRequestTicketExtDTOs.FirstOrDefault()?.TicketId;
            if (ticketId == null) return;
            else
            {
                var existingRequestTicket = await _requestTicketRepository.GetRequestTicketById(ticketId);
                if (existingRequestTicket == null)
                {
                    throw new AppException($"Request ticket item with id {ticketId} not found");
                }
            }
            foreach (var requestTicketExtDto in createUpdateRequestTicketExtDTOs)
            {
                var requestTicketExt = _mapper.Map<RequestTicketExt>(requestTicketExtDto);
                requestTicketExt.RequestTicketExId = await GetNextId();
                requestTicketExt.CreatedDate = DateTime.Now;
                await _repository.AddRequestTicketExt(requestTicketExt);
            }
        }

        public List<RequestTicketExtDTO> GetRequestTicketExts()
        {
            var requestTicketExts = _repository.GetRequestTicketExts();
            return requestTicketExts.Select(requestTicketExt => _mapper.Map<RequestTicketExtDTO>(requestTicketExt)).ToList();
        }
        public async Task<List<RequestTicketExtDTO>> GetRequestTicketExtsForTicket(string requestTicketId)
        {
            var requestTicketExts = await _repository.GetRequestTicketExtsForTicket(requestTicketId);
            return requestTicketExts.Select(requestTicketExt => _mapper.Map<RequestTicketExtDTO>(requestTicketExt)).ToList();
        }
        public async Task<RequestTicketExtDTO> GetRequestTicketExtById(string requestTicketExtId)
        {
            var requestTicketExt = await _repository.GetRequestTicketExtById(requestTicketExtId);
            return _mapper.Map<RequestTicketExtDTO>(requestTicketExt);
        }

        public async Task UpdateRequestTicketExt(string requestTicketExtId, CreateUpdateRequestTicketExtDTO createUpdateRequestTicketExtDTO)
        {
            RequestTicketExt requestTicketExt = await _repository.GetRequestTicketExtById(requestTicketExtId);
            if (requestTicketExt == null)
            {
                throw new AppException("Group not found");
            }
            if (await _customFieldRepository.GetCustomFieldById(createUpdateRequestTicketExtDTO.FieldId) == null)
            {
                throw new AppException("Customer field with id " + createUpdateRequestTicketExtDTO.FieldId + " not found");
            }
            if (await _requestTicketRepository.GetRequestTicketById(createUpdateRequestTicketExtDTO.TicketId) == null)
            {
                throw new AppException("Request ticket with id " + createUpdateRequestTicketExtDTO.TicketId + " not found");
            }
            requestTicketExt = _mapper.Map(createUpdateRequestTicketExtDTO, requestTicketExt);
            await _repository.UpdateRequestTicketExt(requestTicketExt);
        }

        public async Task DeleteRequestTicketExt(string requestTicketExtId)
        {

        }

        public async Task<string> GetNextId()
        {
            RequestTicketExt lastRequestTicketExt = await _repository.GetLastRequestTicketExt();
            int id = 0;
            if (lastRequestTicketExt == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastRequestTicketExt.RequestTicketExId) + 1;
            }
            string requestTicketExId = IDGenerator.GenerateRequestTicketExtId(id);
            return requestTicketExId;
        }
    }
}
