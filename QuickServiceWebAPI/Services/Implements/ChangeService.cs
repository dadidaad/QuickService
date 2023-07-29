using AutoMapper;
using Microsoft.Extensions.Options;
using QuickServiceWebAPI.DTOs.Change;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class ChangeService : IChangeService
    {
        private readonly IChangeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<ServiceItemService> _logger;
        private readonly IAttachmentService _attachmentService;
        public ChangeService(IChangeRepository repository, IMapper mapper,
            IUserRepository userRepository, IGroupRepository groupRepository,
            ILogger<ServiceItemService> logger, IAttachmentService attachmentService)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _logger = logger;
            _attachmentService = attachmentService; 
        }

        public async Task CreateChange(CreateChangeDTO createChangeDTO)
        {
            await ValidateData(createChangeDTO);
            var change = _mapper.Map<Change>(createChangeDTO);
            if(createChangeDTO.AttachmentFile is not null)
            {
                change.Attachment = await _attachmentService.CreateAttachment(createChangeDTO.AttachmentFile);
            }
            change.ChangeId = await GetNextId();
            await _repository.AddChange(change);
        }

        private async Task ValidateData(CreateChangeDTO createChangeDTO)
        {
            var requester = _userRepository.GetUserDetails(createChangeDTO.RequesterId);
            if (requester == null)
            {
                throw new AppException($"User with id: {createChangeDTO.RequesterId} not found");
            }
            if (!string.IsNullOrEmpty(createChangeDTO.AssignerId))
            {
                var assigner = _userRepository.GetUserDetails(createChangeDTO.AssignerId);
                if (assigner == null)
                {
                    throw new AppException($"User with id: {createChangeDTO.AssignerId} not found");
                }
            }
            if (!string.IsNullOrEmpty(createChangeDTO.GroupId))
            {
                var group = _groupRepository.GetGroupById(createChangeDTO.GroupId);
                if (group == null)
                {
                    throw new AppException($"Group with id: {createChangeDTO.GroupId} not found");
                }
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
            string seriveId = IDGenerator.GenerateServiceId(id);
            return seriveId;
        }
    }
}
