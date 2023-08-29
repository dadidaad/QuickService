using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IGroupRepository
    {
        public List<Group> GetGroups();
        public Task<Group> GetGroupById(string groupId);
        public Task AddGroup(Group group);
        public Task AddUserToGroup(string userId, string groupId);
        public Task UpdateGroup(Group group);
        public Task DeleteGroup(Group group);
        public Task<Group> GetLastGroup();
    }
}
