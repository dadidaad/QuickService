using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IUserRepository
    {
        public List<User> GetUsers();
        public Task<User> GetUserDetails(string userId);
        public Task<User> GetUserByEmail(string email);
        public Task AddUser(User user);
        public Task UpdateUser(User updateUser);
        public Task<User> GetLastUser();
        public Task<List<User>> GetUsersByContainString(string containStr, string? groupId);
    }
}
