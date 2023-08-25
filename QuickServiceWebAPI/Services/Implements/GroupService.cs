using AutoMapper;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IBusinessHourRepository _businessHourRepository;
        private readonly IMapper _mapper;
        public GroupService(IGroupRepository repository, IMapper mapper, IUserRepository userRepository, IBusinessHourRepository businessHourRepository)
        {
            _userRepository = userRepository;
            _businessHourRepository = businessHourRepository;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateGroup(CreateUpdateGroupDTO createUpdateGroupDTO)
        {
            var group = _mapper.Map<Group>(createUpdateGroupDTO);
            group.GroupId = await GetNextId();
            group.NeedApprovalByLeader = false;
            group.IsRestricted = false;
            await _repository.AddGroup(group);
        }

        public List<GroupDTO> GetGroups()
        {
            var groups = _repository.GetGroups();
            return groups.Select(group => _mapper.Map<GroupDTO>(group)).ToList();
        }

        public async Task<GroupDTO> GetGroupById(string groupId)
        {
            var group = await _repository.GetGroupById(groupId);
            if (group == null)
            {
                throw new AppException("Group not found");
            }
            return _mapper.Map<GroupDTO>(group);
        }

        public async Task UpdateGroup(string groupId, CreateUpdateGroupDTO createUpdateGroupDTO)
        {
            Group group = await _repository.GetGroupById(groupId);
            if (group == null)
            {
                throw new AppException("Group not found");
            }
            if (await _userRepository.GetUserDetails(createUpdateGroupDTO.GroupLeader) == null)
            {
                throw new AppException("Group leader with id " + createUpdateGroupDTO.GroupLeader + " not found");
            }
            //if (await _businessHourRepository.GetBusinessHourById(createUpdateGroupDTO.BusinessHourId) == null)
            //{
            //    throw new AppException("Business hour with id " + createUpdateGroupDTO.BusinessHourId + " not found");
            //}
            group = _mapper.Map(createUpdateGroupDTO, group);
            await _repository.UpdateGroup(group);
        }

        public async Task AddUserToGroup(string userId, string groupId)
        {
            var group = await _repository.GetGroupById(groupId);
            if (group == null)
            {
                throw new AppException("Group not found");
            }
            var user = await _userRepository.GetUserDetails(userId);
            if (user == null)
            {
                throw new AppException("User with id " + userId + " not found");
            }
            if (!group.Users.Any(user => user.UserId == userId))
            {
                group.Users.Add(user);
            }
            else
            {
                throw new AppException($"Group with id {groupId} already have user with id {userId}");
            }
            await _repository.UpdateGroup(group);
        }

        public async Task DeleteGroup(string groupId)
        {

        }

        public async Task<string> GetNextId()
        {
            Group lastGroup = await _repository.GetLastGroup();
            int id = 0;
            if (lastGroup == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastGroup.GroupId) + 1;
            }
            string groupId = IDGenerator.GenerateGroupId(id);
            return groupId;
        }
    }
}
