using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository repository, ILogger<UserService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task CreateUser(User user)
        {
            user.UserId = await GetNextId();

        }

        public Task<User> DeactiveUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetNextId()
        {
            User lastUser = await _repository.GetLastUser();
            int id = 0;
            if(lastUser == null){
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastUser.UserId) + 1;
            }
            string userId = IDGenerator.GenerateUserId(id);
            return userId;
        }

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
