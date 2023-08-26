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
        public ChangeService(IChangeRepository repository, IMapper mapper,
            IUserRepository userRepository, IGroupRepository groupRepository,
            ILogger<ChangeService> logger, IAttachmentService attachmentService, 
            ISlaService slaService, IRequestTicketRepository requestTicketRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _logger = logger;
            _attachmentService = attachmentService;
            _slaService = slaService;
            _requestTicketRepository = requestTicketRepository;
        }

        public async Task<ChangeDTO> CreateChange(CreateChangeDTO createChangeDTO)
        {
            await ValidateData(createChangeDTO);
            var change = _mapper.Map<Change>(createChangeDTO);                  
            change.ChangeId = await GetNextId();
            change.Status = StatusEnum.Open.ToString();
            change.Priority = PriorityEnum.Low.ToString();
            change.Impact = ImpactEnum.Low.ToString();
            change.CreatedTime = DateTime.Now;
            await _repository.AddChange(change);
            await AddChangeIdToRequestTicket(change.ChangeId, createChangeDTO.RequestTicketIds);


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
            var change = await _repository.GetChangeById(updateChangeDTO.ChangeId);
            if (change == null)
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
                change.Attachment = await _attachmentService.CreateAttachment(updateChangeDTO.AttachmentFile);
            }
            var updateChange = _mapper.Map(updateChangeDTO, change);
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
