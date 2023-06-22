using AutoMapper;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private IJWTUtils _jWTUtils;
        public UserService(IUserRepository repository, ILogger<UserService> logger, IJWTUtils jWTUtils, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _jWTUtils = jWTUtils;
            _mapper = mapper;
        }

        public async Task CreateUser(RegisterDTO registerDTO)
        {

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

        public Task<User> GetUserByEmail(AuthenticateRequestDTO authenticateRequestDTO)
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
