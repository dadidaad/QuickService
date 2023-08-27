using QuickServiceWebAPI.DTOs.User;

namespace QuickServiceWebAPI.Services
{
    public interface IUserService
    {
        public Task<List<UserDTO>> GetUserByContainString(ContainStringDTO containStringDTO);
        public Task<UserDTO> CreateUser(RegisterDTO registerDTO);
        public Task UpdateUser(UpdateUserDTO updateDTO);
        public Task<AuthenticateResponseDTO> Authenticate(AuthenticateRequestDTO authenticateRequestDTO);
        public Task<UserDTO> GetUserById(string userId);
        public Task DeactiveUser(string userId);
        public List<UserDTO> GetUsers();
        public Task AssignRole(AssignRoleDTO assignRoleDTO);
        public Task ChangePassword(ChangePasswordDTO changePasswordDTO);
        public Task ResetPassword(ResetPasswordDTO resetPasswordDTO);
    }
}
