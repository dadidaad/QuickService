using AutoMapper;
using Microsoft.Extensions.Options;
using QuickServiceWebAPI.DTOs.Change;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
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
        private readonly ILogger<ChangeService> _logger;
        private readonly IAttachmentService _attachmentService;
        public ChangeService(IChangeRepository repository, IMapper mapper,
            IUserRepository userRepository, IGroupRepository groupRepository,
            ILogger<ChangeService> logger, IAttachmentService attachmentService)
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
            change.CreatedTime = DateTime.Now;
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
            string changeId = IDGenerator.GenerateServiceId(id);
            return changeId;
        }

        private void ActivitiesEachStatus(Change change, UpdateChangePropertiesDTO updateChangePropertiesDTO)
        {
            var currentStatusChange = change.Status.ToEnum(StatusChangeEnum.Open);
            var updateStatusChange = updateChangePropertiesDTO.Status.ToEnum(StatusChangeEnum.Open);
            if (currentStatusChange == updateStatusChange)
            {
                return;
            }
            if((int)updateStatusChange - (int)currentStatusChange != 1)
            {
                throw new AppException("Must update to next status");
            }
            if(updateStatusChange == StatusChangeEnum.Planning)
            {
                if(//updateChangePropertiesDTO.GroupId == null ||
                   updateChangePropertiesDTO.AssignerId == null)
                {
                    throw new AppException("Must assign to a group and agent");
                }
            }
            if(updateStatusChange == StatusChangeEnum.AwaitingApproval && updateChangePropertiesDTO.PlanningDTO == null)
            {
                throw new AppException("Must have a plan");
            }
            if(updateStatusChange == StatusChangeEnum.PendingRelease && !change.IsApprovedByCab)
            {
                throw new AppException("Change must be approved by CAB before proceeding to the next status.");
            }
            
        }

        public async Task UpdateChange(UpdateChangeDTO updateChangeDTO)
        {
            var change = await _repository.GetChangeById(updateChangeDTO.ChangeId);
            if(change == null)
            {
                throw new AppException($"Change with id: {updateChangeDTO.ChangeId} not found");
            }
            var requester = await _userRepository.GetUserDetails(updateChangeDTO.RequesterId);
            if (requester == null)
            {
                throw new AppException($"User with id: {updateChangeDTO.RequesterId} not found");
            }

            var updateChange = _mapper.Map(updateChangeDTO, change);
            await _repository.UpdateChange(updateChange);
        }

        public async Task UpdateChangeProperties(UpdateChangePropertiesDTO updateChangePropertiesDTO)
        {
            var change = await _repository.GetChangeById(updateChangePropertiesDTO.ChangeId);
            if (change == null)
            {
                throw new AppException($"Change with id: {updateChangePropertiesDTO.ChangeId} not found");
            }
            if(updateChangePropertiesDTO.ChangeType != change.ChangeType)
            {
                updateChangePropertiesDTO.Status = StatusChangeEnum.Open.ToString();
            }
            else
            {
                ActivitiesEachStatus(change, updateChangePropertiesDTO);
            }
            var assigner = await _userRepository.GetUserDetails(updateChangePropertiesDTO.AssignerId);
            if (assigner == null)
            {
                throw new AppException($"User with id: {updateChangePropertiesDTO.AssignerId} not found");
            }
            var updateChange = _mapper.Map(updateChangePropertiesDTO, change);
            await _repository.UpdateChange(updateChange);
        }

        public async Task<List<ChangeDTO>> GetAllChanges()
        {
            var changes = await _repository.GetChanges();
            return changes.Select(x => new ChangeDTO()
            {
                ChangeId = x.ChangeId,
                Title = x.Title,
                Requester = _mapper.Map<UserDTO>(x.Requester),
                Status = x.Status,
                Priority = x.Priority,
                Assigner = _mapper.Map<UserDTO>(x.Assigner)
            }).ToList();
        }

        public async Task<ChangeDTO> GetChange(string changeId)
        {
            var change = await _repository.GetChangeById(changeId);
            if(change == null)
            {
                throw new AppException($"Change with id: {changeId} not found");
            }
            return _mapper.Map<ChangeDTO>(change);
        }

        public Task DeleteChange(string changeId)
        {
            throw new NotImplementedException();
        }
    }
}
