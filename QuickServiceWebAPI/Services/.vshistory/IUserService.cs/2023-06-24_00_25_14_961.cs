﻿using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Services
{
    public interface IUserService
    {
        public Task CreateUser(RegisterDTO registerDTO);
        public Task UpdateUser(UpdateDTO updateDTO);
        public Task<AuthenticateResponseDTO> Authenticate(AuthenticateRequestDTO authenticateRequestDTO);
        public Task<User> GetUserById(string userId);
        public Task<User> DeactiveUser(string userId);
        public List<User> GetUsers();
        public Task<string> GetNextId();
        Task<string> UpdateAvatar(IFormFile image, string email);
    }
}
