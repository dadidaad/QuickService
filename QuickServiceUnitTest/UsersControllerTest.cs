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
                }).CreateMapper(),
                Options.Create(new AzureStorageConfig()),
                new Mock<IRoleRepository>().Object
            );

            _controller = new UsersController(userService);
        }

        [Fact]
        public void GetAllUser_ReturnsOkResult()
        {
            var result = _controller.GetAllUser();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<UserDTO>;

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<UserDTO>>(resultType.Value);
            Assert.Equal(10, resultList.Count);
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
