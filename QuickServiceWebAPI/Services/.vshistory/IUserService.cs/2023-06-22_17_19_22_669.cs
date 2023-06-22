using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Services
{
    public interface IUserService
    {
        public Task CreateUser(User user);
        public Task UpdateUser(User user);
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetUserById(string userId);
        public Task<User> DeactiveUser(string userId);
        public IEnumerable<User> GetUsers();
    }
}
