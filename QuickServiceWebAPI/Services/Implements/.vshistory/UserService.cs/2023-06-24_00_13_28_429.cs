﻿using AutoMapper;
using BCrypt.Net;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Helpers;
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
        private readonly AzureStorageConfig _storageConfig;
        public UserService(IUserRepository repository, ILogger<UserService> logger, IJWTUtils jWTUtils, IMapper mapper, AzureStorageConfig storageConfig)
        {
            _repository = repository;
            _logger = logger;
            _jWTUtils = jWTUtils;
            _mapper = mapper;
            _storageConfig = storageConfig;
        }

        public async Task CreateUser(RegisterDTO registerDTO)
        {
            if (await _repository.GetUserByEmail(registerDTO.Email) != null)
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
            if(user == null  || !BCrypt.Net.BCrypt.Verify(authenticateRequestDTO.Password, user.Password))
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

        public List<User> GetUsers()
        {
            return _repository.GetUsers();
        }

        public async Task UpdateUser(UpdateDTO updateDTO)
        {
            User user = await _repository.GetUserByEmail(updateDTO.Email);
            if(user == null)
            {
                throw new AppException("User not found");
            }
            if(updateDTO.Avatar != null)
            {

            }
        }

        async Task UpdateAvatar(IFormFile image, string userId)
        {
            bool isUploaded = false;

            try
            {
                if (_storageConfig.AccountKey == string.Empty || _storageConfig.AccountName == string.Empty)
                    throw new AppException("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                if (_storageConfig.ImageContainer == string.Empty)
                    throw new AppException("Please provide a name for your image container in the azure blob storage");

                if (CloudHelper.IsImage(image))
                {
                    if(image.Length > 0 && image.Length <= 2097152)
                    {
                        using(Stream stream = image.OpenReadStream())
                        {
                            isUploaded = await CloudHelper.UploadImageToStorage(stream, userId, _storageConfig);
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

                if (isUploaded)
                {
                    if(_storageConfig.ThumbnailContainer != string.Empty)
                    {
                        return 
                    }
                }
            }
        }
    }
}
