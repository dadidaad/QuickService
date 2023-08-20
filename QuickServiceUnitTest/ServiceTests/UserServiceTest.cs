using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using QuickServiceUnitTest.ServiceTests.DerivedClasses;
using QuickServiceUnitTest.ServiceTests.Fixtures;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTest
{
    [Collection("UserServiceTest")]
    public class UserServiceTest
    {
        private readonly UserServiceTestFixture _fixture;

        public UserServiceTest()
        {
            _fixture = new UserServiceTestFixture();
        }

        [Fact]
        public async Task CreateUser_UniqueEmail_Success()
        {
            // Arrange
            var userService = _fixture.UserService;

            var registerDTO = new RegisterDTO
            {
                Email = "newuser@example.com",
                Password = "securepassword"
            };

            _fixture.MockUserRepository.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((User?)null);

            var hashedPassword = "hashedpassword"; // Replace with the expected hashed password
            _fixture.MockMapper.Setup(mapper => mapper.Map<User>(It.IsAny<RegisterDTO>())).Returns(new User());
            _fixture.MockMapper.Setup(mapper => mapper.Map<User>(registerDTO)).Returns(new User { Password = hashedPassword });
            // Act
            await userService.CreateUser(registerDTO);

            // Assert
            _fixture.MockUserRepository.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task CreateUser_DuplicateEmail_ThrowsException()
        {

            var userService = _fixture.UserService;

            var registerDTO = new RegisterDTO
            {
                Email = "existinguser@example.com",
                Password = "securepassword"
            };

            var existingUser = new User(); // Replace with an existing user
            existingUser.Email = "existinguser@example.com";
            _fixture.MockUserRepository.Setup(repo => repo.GetUserByEmail(registerDTO.Email)).ReturnsAsync(existingUser);

            // Act & Assert
            await Assert.ThrowsAsync<AppException>(async () => await userService.CreateUser(registerDTO));
        }


        [Fact]
        public async Task Authenticate_ValidCredentials_ReturnsValidResponse()
        {

            var userService = _fixture.UserService;
            var requestDTO = new AuthenticateRequestDTO
            {
                Email = "test@example.com",
                Password = "testpassword"
            };

            var userFromRepository = new User
            {
                UserId = "USER1",
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("testpassword")
            };

            var responseDTO = new AuthenticateResponseDTO
            {
                Email = "test@example.com",
                Token = "your_generated_token"
            };

            _fixture.MockUserRepository.Setup(repo => repo.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(userFromRepository);

            _fixture.MockJWTUtils.Setup(utils => utils.GenerateToken(It.IsAny<User>()))
                .ReturnsAsync("your_generated_token");

            _fixture.MockMapper.Setup(map => map.Map<AuthenticateResponseDTO>(It.IsAny<User>()))
                .Returns(responseDTO);

            // Act
            var result = await userService.Authenticate(requestDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(responseDTO.Email, result.Email);
            Assert.Equal(responseDTO.Token, result.Token);
        }

        [Fact]
        public async Task Authenticate_InvalidCredentials_ThrowsAppException()
        {
            var userService = _fixture.UserService;

            var requestDTO = new AuthenticateRequestDTO
            {
                Email = "test@example.com",
                Password = "invalidpassword"
            };

            var userFromRepository = new User
            {
                UserId = "USER1",
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("testpassword")
            };

            _fixture.MockUserRepository.Setup(repo => repo.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(userFromRepository);

            // Act and Assert
            await Assert.ThrowsAsync<AppException>(async () => await userService.Authenticate(requestDTO));
        }

        [Fact]
        public async Task Authenticate_UserNotFound_ThrowsAppException()
        {
            var userService = _fixture.UserService;

            var requestDTO = new AuthenticateRequestDTO
            {
                Email = "nonexistent@example.com",
                Password = "testpassword"
            };

            _fixture.MockUserRepository.Setup(repo => repo.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync((User)null); // Simulate user not found

            // Act and Assert
            await Assert.ThrowsAsync<AppException>(async () => await userService.Authenticate(requestDTO));
        }


        [Fact]
        public async Task UpdateUser_ValidData_UpdatesUserSuccessfully()
        {

            var testableUserService = new TestableUserService(
                _fixture.MockUserRepository.Object,
                _fixture.MockLogger.Object,
                _fixture.MockJWTUtils.Object,
                _fixture.MockMapper.Object,
                _fixture.MockStorageConfig.Object,
                _fixture.MockRoleRepository.Object
            );

            var updateDTO = new UpdateUserDTO
            {
                UserId = "USER1",
                AvatarUpload = GetEmptyFile(),
                WallpaperUpload = GetEmptyFile()
                // Other properties
            };

            var existingUser = new User
            {
                UserId = "USER1",
                // Other properties
            };

            _fixture.MockUserRepository.Setup(repo => repo.GetUserDetails(It.IsAny<string>()))
                .ReturnsAsync(existingUser);

            _fixture.MockMapper.Setup(map => map.Map(It.IsAny<UpdateUserDTO>(), It.IsAny<User>()))
                .Returns(existingUser);
            // Act
            await testableUserService.UpdateUser(updateDTO);

            // Assert
            _fixture.MockUserRepository.Verify(repo => repo.UpdateUser(existingUser, existingUser), Times.Once);
        }

        [Fact]
        public async Task UpdateUser_UserNotFound_ThrowsAppException()
        {

            var userService = _fixture.UserService;

            var updateDTO = new UpdateUserDTO
            {
                UserId = "User1",
                // Other properties
            };

            _fixture.MockUserRepository.Setup(repo => repo.GetUserDetails(It.IsAny<string>()))
                .ReturnsAsync((User)null); // Simulate user not found

            // Act and Assert
            await Assert.ThrowsAsync<AppException>(async () => await userService.UpdateUser(updateDTO));
        }


        private FormFile GetEmptyFile()
        {
            return new FormFile(
                baseStream: new System.IO.MemoryStream(), // Use an empty memory stream
                baseStreamOffset: 0,
                length: 0,
                name: "emptyFile",
                fileName: "emptyFile.txt");
        }
    }
}
