using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.Service;
using QuickServiceWebAPI.DTOs.ServiceType;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest
{
    public class UsersControllerTest
    {
        private readonly UsersController _controller;

        public UsersControllerTest()
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<UserRepository>>();
            var userService = new UserService(
                new UserRepository(dbContext, mockLogger.Object),
                new Mock<ILogger<UserService>>().Object,
                new Mock<IJWTUtils>().Object,
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Service, ServiceDTO>();
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<Group, GroupDTO>();
                    cfg.CreateMap<ServiceType, ServiceTypeDTO>();
                    cfg.CreateMap<RegisterDTO, User>();
                }).CreateMapper(),
                Options.Create(new AzureStorageConfig()),
                new Mock<IRoleRepository>().Object
            );

            _controller = new UsersController(userService);
        }

        //[Fact]
        //public async Task CreateUser_ValidInput_ReturnsOk()
        //{
        //    var registerDTO = new RegisterDTO
        //    {
        //        Email = "Email@gmail.com",
        //        Password = "Password",
        //        FirstName = "FirstName",
        //        MiddleName = "MiddleName",
        //        LastName = "LastName"
        //    };

        //    var result = await _controller.CreateUser(registerDTO);

        //    Assert.IsType<OkObjectResult>(result);
        //}

        [Fact]
        public async Task CreateUser_MissingEmail_ReturnsOk()
        {
            var registerDTO = new RegisterDTO
            {
                Password = "Password",
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName"
            };

            var context = new ValidationContext(registerDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(registerDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Email field is required.");
        }

        [Fact]
        public async Task CreateUser_InvalidEmail_ReturnsOk()
        {
            var registerDTO = new RegisterDTO
            {
                Email = "Email",
                Password = "Password",
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName"
            };

            var context = new ValidationContext(registerDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(registerDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Email field is not a valid e-mail address.");
        }

        [Fact]
        public async Task CreateUser_InvalidEmailLength_ReturnsOk()
        {
            var registerDTO = new RegisterDTO
            {
                Email = new string('E', 101),
                Password = "Password",
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName"
            };

            var context = new ValidationContext(registerDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(registerDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field Email must be a string or array type with a maximum length of '100'.");
        }

        [Fact]
        public async Task CreateUser_MissingPassword_ReturnsOk()
        {
            var registerDTO = new RegisterDTO
            {
                Email = "Email@gmail.com",
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName"
            };

            var context = new ValidationContext(registerDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(registerDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Password field is required.");
        }

        [Fact]
        public async Task CreateUser_InvalidPassword_ReturnsOk()
        {
            var registerDTO = new RegisterDTO
            {
                Email = "Email",
                Password = new string('a', 101),
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName"
            };

            var context = new ValidationContext(registerDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(registerDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field Password must be a string or array type with a maximum length of '100'.");
        }

        [Fact]
        public async Task CreateUser_InvalidFirstName_ReturnsOk()
        {
            var registerDTO = new RegisterDTO
            {
                Email = "Email",
                Password = "Password",
                FirstName = new string('a', 21),
                MiddleName = "MiddleName",
                LastName = "LastName"
            };

            var context = new ValidationContext(registerDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(registerDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field FirstName must be a string or array type with a maximum length of '20'.");
        }

        [Fact]
        public async Task CreateUser_InvalidMiddleName_ReturnsOk()
        {
            var registerDTO = new RegisterDTO
            {
                Email = "Email",
                Password = "Password",
                FirstName = "FirstName",
                MiddleName = new string('a', 21),
                LastName = "LastName"
            };

            var context = new ValidationContext(registerDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(registerDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field MiddleName must be a string or array type with a maximum length of '20'.");
        }

        [Fact]
        public async Task CreateUser_InvalidLastName_ReturnsOk()
        {
            var registerDTO = new RegisterDTO
            {
                Email = "Email",
                Password = "Password",
                FirstName = "FirstName",
                MiddleName = "LastName",
                LastName = new string('a', 21)
            };

            var context = new ValidationContext(registerDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(registerDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field LastName must be a string or array type with a maximum length of '20'.");
        }

        [Fact]
        public async Task GetUserById_ReturnsOkResult()
        {
            //Arrange
            string valid_userId = "USER000001";
            //string inValid_userId = "ABCDA123";

            //Act
            //var errorResult = await _controller.GetById(inValid_userId);
            var successResult = await _controller.GetById(valid_userId);

            var successModel = successResult as OkObjectResult;
            var fetchedUser= successModel.Value as UserDTO;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<AppException>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<UserDTO>(fetchedUser);
        }
    }
}
