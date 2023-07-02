using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Services
{
    public interface IUserService
    {
        public Task CreateUser(RegisterDTO registerDTO);
        public Task UpdateUser(UpdateDTO updateDTO);
        public Task<AuthenticateResponseDTO> Authenticate(AuthenticateRequestDTO authenticateRequestDTO);
        public Task<UserDTO> GetUserById(string userId);
        public Task<User> DeactiveUser(string userId);
        public List<UserDTO> GetUsers();
        Task<string> UpdateAvatar(IFormFile image, string userId);
    }
}
