using AutoMapper;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.ServiceType;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;
        private readonly IMapper _mapper;
        public GroupService(IGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateGroup(CreateUpdateGroupDTO createUpdateGroupDTO)
        {
            var group = _mapper.Map<Group>(createUpdateGroupDTO);
            group.GroupId = await GetNextId();
            await _repository.AddGroup(group);
        }

        public List<GroupDTO> GetGroups()
        {
            var groups = _repository.GetGroups();
            return groups.Select(group => _mapper.Map<GroupDTO>(group)).ToList();
        }

        public async Task UpdateGroup(string groupId, CreateUpdateGroupDTO createUpdateGroupDTO)
        {
            Group group = await _repository.GetGroupById(groupId);
            if (group == null)
            {
                throw new AppException("ServiceType not found");
            }
            if (!String.IsNullOrEmpty(createUpdateGroupDTO.GroupName))
            {
                group.GroupName = createUpdateGroupDTO.GroupName;
            }
            if (!String.IsNullOrEmpty(createUpdateGroupDTO.Description))
            {
                group.Description = createUpdateGroupDTO.Description;
            }
            if (!String.IsNullOrEmpty(createUpdateGroupDTO.GroupLeader))
            {
                group.GroupLeader = createUpdateGroupDTO.GroupLeader;
            }
            if (!String.IsNullOrEmpty(createUpdateGroupDTO.BusinessHourId))
            {
                group.BusinessHourId = createUpdateGroupDTO.BusinessHourId;
            }
            group = _mapper.Map<CreateUpdateGroupDTO, Group>(createUpdateGroupDTO, group);
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
