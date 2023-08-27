using AutoMapper;
using QuickServiceWebAPI.DTOs.Change;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class ChangeService : IChangeService
    {
        private readonly IChangeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<ChangeService> _logger;
        private readonly IAttachmentService _attachmentService;
        private readonly ISlaService _slaService;
        private readonly IRequestTicketRepository _requestTicketRepository;
        private readonly IRequestTicketHistoryService _requestTicketHistoryService;
        private readonly IRequestTicketHistoryRepository _requestTicketHistoryRepository;
        public ChangeService(IChangeRepository repository, IMapper mapper,
            IUserRepository userRepository, IGroupRepository groupRepository,
            ILogger<ChangeService> logger, IAttachmentService attachmentService, 
            ISlaService slaService, IRequestTicketRepository requestTicketRepository,
            IRequestTicketHistoryService requestTicketHistoryService,
            IRequestTicketHistoryRepository requestTicketHistoryRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _logger = logger;
            _attachmentService = attachmentService;
            _slaService = slaService;
            _requestTicketRepository = requestTicketRepository;
            _requestTicketHistoryService = requestTicketHistoryService;
            _requestTicketHistoryRepository = requestTicketHistoryRepository;
        }

        public async Task<ChangeDTO> CreateChange(CreateChangeDTO createChangeDTO)
        {
            var requester = await _userRepository.GetUserDetails(createChangeDTO.RequesterId);
            if (requester == null)
            {
                throw new AppException($"User with id {createChangeDTO.RequesterId} not found");
            }
            await ValidateData(createChangeDTO);
            var change = _mapper.Map<Change>(createChangeDTO);                  
            change.ChangeId = await GetNextId();         
            change.Status = StatusEnum.Open.ToString();
            change.Priority = PriorityEnum.Low.ToString();
            change.Impact = ImpactEnum.Low.ToString();
            change.CreatedTime = DateTime.Now;
            change.RequesterId = requester.UserId;
            await _repository.AddChange(change);
            await AddChangeIdToRequestTicket(change.ChangeId, createChangeDTO.RequestTicketIds);

            var historyFirst = new RequestTicketHistory
            {

                RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory(),
                ChangeId = change.ChangeId,
                Content = $"Create change",
                LastUpdate = change.CreatedTime,
                UserId = change.RequesterId
            };
            await _requestTicketHistoryRepository.AddRequestTicketHistory(historyFirst);

            var historySecond = new RequestTicketHistory
            {

                RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory(),
                ChangeId = change.ChangeId,
                Content = $"Assigned to",
                LastUpdate = change.CreatedTime,
                UserId = change.AssigneeId
            };
            await _requestTicketHistoryRepository.AddRequestTicketHistory(historySecond);

            var createdChangeDTO = await _repository.GetChangeById(change.ChangeId);
            return _mapper.Map<ChangeDTO>(createdChangeDTO);
        }

        public async Task AddChangeIdToRequestTicket(string changeId, List<String> RequestTicketIds)
        {
            foreach (var requestTicketId in RequestTicketIds)
            {
                var requestTicket = await _requestTicketRepository.GetRequestTicketById(requestTicketId);
                if (requestTicket == null)
                {
                    throw new AppException($"Request ticket with id: {requestTicketId} not found");

                }
                requestTicket.ChangeId = changeId;
                await _requestTicketRepository.UpdateRequestTicket(requestTicket);
            }
        }

        private async Task ValidateData(CreateChangeDTO createChangeDTO)
        {
            var assigner = await _userRepository.GetUserDetails(createChangeDTO.AssigneeId);
            if (assigner == null)
            {
               throw new AppException($"User with id: {createChangeDTO.AssigneeId} not found");
            }
            var sla = await _slaService.GetSlaById(createChangeDTO.Slaid);
            if (sla == null)
            {
                throw new AppException($"Sla with id: {createChangeDTO.Slaid} not found");
            }
        }

        private async Task<string> GetNextId()
        {
            Change lastChange = await _repository.GetLastChange();
            int id = 0;
            if (lastChange == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastChange.ChangeId) + 1;
            }
            string changeId = IDGenerator.GenerateChangeId(id);
            return changeId;
        }

        public async Task UpdateChange(UpdateChangeDTO updateChangeDTO)
        {
            var existingChange = await _repository.GetChangeById(updateChangeDTO.ChangeId);
            if (existingChange == null)
            {
                throw new AppException($"Change with id: {updateChangeDTO.ChangeId} not found");
            }
            var assignee = await _userRepository.GetUserDetails(updateChangeDTO.AssigneeId);
            if (assignee == null)
            {
                throw new AppException($"User with id: {updateChangeDTO.AssigneeId} not found");
            }
            if (updateChangeDTO.AttachmentFile is not null)
            {
                existingChange.Attachment = await _attachmentService.CreateAttachment(updateChangeDTO.AttachmentFile);
            }
            if (existingChange.AssigneeId != updateChangeDTO.AssigneeId)
            {
                var history = new RequestTicketHistory();
                history.Content = $"Assigned to";
                history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
                history.ChangeId = updateChangeDTO.ChangeId;
                history.LastUpdate = DateTime.Now;
                history.UserId = updateChangeDTO.AssigneeId;
                await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
            }


            if (updateChangeDTO.Status == StatusEnum.Resolved.ToString())
            {
                var history = new RequestTicketHistory();
                history.Content = $"Change is Completed";
                history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
                history.ChangeId = existingChange.ChangeId;
                history.LastUpdate = DateTime.Now;
                history.UserId = existingChange.AssigneeId;
                await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
            }

            if (existingChange.Status != updateChangeDTO.Status && updateChangeDTO.Status != StatusEnum.Resolved.ToString())
            {
                var history = new RequestTicketHistory();
                history.Content = $"Change Status to {updateChangeDTO.Status}";
                history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
                history.ChangeId = existingChange.ChangeId;
                history.LastUpdate = DateTime.Now;
                history.UserId = existingChange.AssigneeId;
                await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
            }

            if (existingChange.Impact != updateChangeDTO.Impact)
            {
                var history = new RequestTicketHistory();
                history.Content = $"Change Impact to {updateChangeDTO.Impact}";
                history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
                history.ChangeId = existingChange.ChangeId;
                history.LastUpdate = DateTime.Now;
                history.UserId = existingChange.AssigneeId;
                await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
            }

            if (existingChange.Priority != updateChangeDTO.Priority)
            {
                var history = new RequestTicketHistory();
                history.Content = $"Change Priority to {updateChangeDTO.Priority}";
                history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
                history.ChangeId = existingChange.ChangeId;
                history.LastUpdate = DateTime.Now;
                history.UserId = existingChange.AssigneeId;
                await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
            }

            var updateChange = _mapper.Map(updateChangeDTO, existingChange);
            await _repository.UpdateChange(updateChange);
        }

        public async Task<List<ChangeDTO>> GetAllChanges()
        {
            var changes = await _repository.GetChanges();
            return changes.Select(change => _mapper.Map<ChangeDTO>(change)).ToList();
        }

        public async Task<ChangeDTO> GetChange(string changeId)
        {
            var change = await _repository.GetChangeById(changeId);
            if (change == null)
            {
                throw new AppException($"Change with id: {changeId} not found");
            }
            return _mapper.Map<ChangeDTO>(change);
        }

        public Task DeleteChange(string changeId)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<TicketQueryAdminDTO>> GetRequestTicketsQueryAdmin(QueryDTO queryDto)
        //{
        //    return await _repository.GetRequestTicketsQueryAdmin(queryDto);
        //}
    }
}
