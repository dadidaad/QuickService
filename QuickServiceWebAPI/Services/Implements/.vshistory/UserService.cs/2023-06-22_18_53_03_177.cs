using AutoMapper;
using BCrypt.Net;
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
            if (_repository.GetUserByEmail(registerDTO.Email) != null)
            {
                throw new AppException("Email " + registerDTO.Email + " is already taken");
            }
            var user = _mapper.Map<User>(registerDTO);
            user.Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);
            user.CreatedTime = DateTime.Now;
            user.UserId =  await GetNextId();
            await _repository.AddUser(user);
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

        public async Task<AuthenticateResponseDTO> Authenticate(AuthenticateRequestDTO authenticateRequestDTO)
        {
            var user = await _repository.GetUserByEmail(authenticateRequestDTO.Email);
            if(user == null  || BCrypt.Net.BCrypt.Verify(authenticateRequestDTO.Password, user.Password))
            {
                throw new AppException("Email or password is incorrect");
            }
            var response = _mapper.Map<AuthenticateResponseDTO>(user);
            response.Token = _jWTUtils.GenerateToken(user);
            return response;
        }

        public async Task<User> GetUserById(string userId)
        {
            User user = await _repository.GetUserDetails(userId);
            if(user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return user;
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
