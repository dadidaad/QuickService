﻿using QuickServiceWebAPI.DTOs.Group;

namespace QuickServiceWebAPI.Services
{
    public interface IGroupService
    {
        public List<GroupDTO> GetGroups();
        public Task<GroupDTO> GetGroupById(string groupId);
        public Task AddUserToGroup(string userId, string groupId);
        public Task CreateGroup(CreateUpdateGroupDTO createUpdateGroupDTO);
        public Task UpdateGroup(string groupId, CreateUpdateGroupDTO createUpdateGroupDTO);
        public Task DeleteGroup(string groupId);
        public Task<string> GetNextId();
    }
}
