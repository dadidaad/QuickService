﻿using AutoMapper;
using Microsoft.Extensions.Options;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Helpers;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;
using UpdateUserDTO = QuickServiceWebAPI.DTOs.User.UpdateUserDTO;

namespace QuickServiceWebAPI.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private IJWTUtils _jWTUtils;
        private readonly AzureStorageConfig _storageConfig;
        private readonly IRoleRepository _roleRepository;
        public UserService(IUserRepository repository, ILogger<UserService> logger, IJWTUtils jWTUtils, IMapper mapper, IOptions<AzureStorageConfig> storageConfig, IRoleRepository roleRepository)
        {
            _repository = repository;
            _logger = logger;
            _jWTUtils = jWTUtils;
            _mapper = mapper;
            _storageConfig = storageConfig.Value;
            _roleRepository = roleRepository;
        }

        public async Task<UserDTO> CreateUser(RegisterDTO registerDTO)
        {
            if (await _repository.GetUserByEmail(registerDTO.Email) != null)
            {
                throw new AppException("Email " + registerDTO.Email + " is already taken");
            }
            var user = _mapper.Map<User>(registerDTO);
            user.Password = HashPassword(registerDTO.Password);
            user.CreatedTime = DateTime.Now;
            user.UserId = await GetNextId();
            await _repository.AddUser(user);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task DeactiveUser(string userId)
        {
            var existingUser = await _repository.GetUserDetails(userId);
            if (existingUser == null)
            {
                throw new AppException($"User with id {userId} not found");
            }
            if (existingUser.IsActive.HasValue && existingUser.IsActive.Value)
            {
                existingUser.IsActive = false;
            }
            else if (existingUser.IsActive.HasValue && !existingUser.IsActive.Value)
            {
                existingUser.IsActive = true;
            }
            await _repository.UpdateUser(existingUser);
        }

        private async Task<string> GetNextId()
        {
            User lastUser = await _repository.GetLastUser();
            int id = 0;
            if (lastUser == null)
            {
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
            if (user == null || !BCrypt.Net.BCrypt.Verify(authenticateRequestDTO.Password, user.Password))
            {
                throw new AppException("Email or password is incorrect");
            }
            if (!user.IsActive.HasValue || !user.IsActive.Value)
            {
                throw new AppException($"User with email {authenticateRequestDTO.Email} has been deactive from system");
            }
            var response = _mapper.Map<AuthenticateResponseDTO>(user);
            response.Token = await _jWTUtils.GenerateToken(user);
            return response;
        }

        public async Task<UserDTO> GetUserById(string userId)
        {
            User user = await _repository.GetUserDetails(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return _mapper.Map<UserDTO>(user);
        }

        public List<UserDTO> GetUsers()
        {
            //return _repository.GetUsers();
            var users = _repository.GetUsers();
            return users.Select(user => _mapper.Map<UserDTO>(user)).ToList();
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task UpdateUser(UpdateUserDTO updateDTO)
        {
            User existingUser = await _repository.GetUserDetails(updateDTO.UserId);
            if (existingUser == null)
            {
                throw new AppException("User not found");
            }
            string? avatarPath = await GetPathImpageUpload(updateDTO.AvatarUpload, updateDTO.UserId, _storageConfig.ImageContainer);
            string? wallpaperPath = await GetPathImpageUpload(updateDTO.WallpaperUpload, updateDTO.UserId, _storageConfig.WallpaperContainer);
            var updateUser = _mapper.Map(updateDTO, existingUser);
            if (!string.IsNullOrEmpty(avatarPath))
            {
                updateUser.Avatar = avatarPath;
            }
            if (!string.IsNullOrEmpty(wallpaperPath))
            {
                updateUser.WallPaper = wallpaperPath;
            }
            await _repository.UpdateUser(updateUser);
        }

        protected virtual async Task<string?> GetPathImpageUpload(IFormFile image, string userId, string container)
        {
            if (image != null && CloudHelper.IsImage(image))
            {
                return await UpdateImage(image, userId, container);
            }
            return null;
        }
        private async Task<string> UpdateImage(IFormFile image, string userId, string container)
        {
            string? filePath = null;

            try
            {
                if (_storageConfig.AccountKey == string.Empty || _storageConfig.AccountName == string.Empty)
                    throw new AppException("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                if (_storageConfig.ImageContainer == string.Empty)
                    throw new AppException("Please provide a name for your image container in the azure blob storage");

                if (CloudHelper.IsImage(image))
                {
                    if (image.Length > 0 && image.Length <= 2097152)
                    {
                        using (Stream stream = image.OpenReadStream())
                        {
                            string fileName = userId + Path.GetExtension(image.FileName);
                            filePath = await CloudHelper.UploadFileToStorage(stream, fileName, _storageConfig, container);
                        }
                    }
                    else
                    {
                        throw new AppException("File size is not valid");
                    }
                }
                else
                {
                    throw new AppException("Unsupported media format");
                }

                if (filePath != null)
                {
                    return filePath;
                }
                else
                {
                    throw new AppException("Error when try to upload image!!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new AppException("Error when try to upload image!!");
            }
        }

        public async Task AssignRole(AssignRoleDTO assignRoleDTO)
        {
            var existingUser = await _repository.GetUserDetails(assignRoleDTO.UserId);
            if (existingUser == null)
            {
                throw new AppException("User not found");
            }
            var existingRole = await _roleRepository.GetRoleById(assignRoleDTO.RoleId);
            if (existingRole == null)
            {
                throw new AppException("Role not found");
            }
            var updateUser = _mapper.Map<User>(existingUser);
            updateUser.RoleId = assignRoleDTO.RoleId;
            await _repository.UpdateUser(updateUser);
        }

        public async Task ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var existingUser = await _repository.GetUserDetails(changePasswordDTO.UserId);
            if (existingUser == null)
            {
                throw new AppException("User not found");
            }
            if (!BCrypt.Net.BCrypt.Verify(changePasswordDTO.OldPassword, existingUser.Password))
            {
                throw new AppException("Old password not correct");
            }
            var newHassPassword = HashPassword(changePasswordDTO.NewPassword);
            var updateUser = _mapper.Map<User>(existingUser);
            updateUser.Password = newHassPassword;
            await _repository.UpdateUser(updateUser);
        }

        private const string DEFAULT_PASSWORD = "123";
        public async Task ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var existingUser = await _repository.GetUserDetails(resetPasswordDTO.UserId);
            if (existingUser == null)
            {
                throw new AppException("User not found");
            }
            var newHassPassword = HashPassword(DEFAULT_PASSWORD);
            var updateUser = _mapper.Map<User>(existingUser);
            updateUser.Password = newHassPassword;
            await _repository.UpdateUser(updateUser);
        }

        public async Task<List<UserDTO>> GetUserByContainString(ContainStringDTO containStringDTO)
        {
            var listUserRelated = await _repository.GetUsersByContainString(containStringDTO.containStr, containStringDTO.GroupId);
            return _mapper.Map<List<UserDTO>>(listUserRelated);
        }
    }
}
