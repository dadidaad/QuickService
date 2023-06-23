using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUsers();
        public Task<User> GetUserDetails(string userId);
        public Task<User> GetUserByEmail(string email);
        public Task AddUser(User user);
        public Task UpdateUser(User user);
        public Task<User> DeactiveUser(string userId);
        public Task<User> GetLastUser();
    }
}
