using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.CustomField;

namespace QuickServiceUnitTest
{
    public class GroupsControllerTest
    {
        private readonly GroupsController _controller;
        public GroupsControllerTest() 
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<GroupRepository>>();

            var groupService = new GroupService(
                new GroupRepository(dbContext, mockLogger.Object),
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Group, GroupDTO>();
                    cfg.CreateMap<User, UserDTO>();
                }).CreateMapper(),
                new Mock<IUserRepository>().Object,
                new Mock<IBusinessHourRepository>().Object               
            );
            _controller = new GroupsController(groupService);
        }

        [Fact]
        public async Task GetAllGroup_ReturnsOkResult()
        {
            var result = _controller.GetAllGroup();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<GroupDTO>;

            Assert.NotNull(result);
            Assert.IsType<List<GroupDTO>>(resultType.Value);
            Assert.Equal(16, resultList.Count);
        }

        [Fact]
        public async Task GetGroupById_ReturnsOkResult()
        {
            //Arrange
            string valid_groupId = "GROU000001";
            //string inValid_groupId = "ABCDA123";

            //Act
            //var errorResult = await _controller.GetGroupById(inValid_groupId);
            var successResult = await _controller.GetGroupById(valid_groupId);

            var successModel = successResult as OkObjectResult;
            var fetchedGroup = successModel.Value as GroupDTO;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<OkObjectResult>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<GroupDTO>(fetchedGroup);
        }
    }
}
