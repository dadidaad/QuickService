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
using System.ComponentModel.DataAnnotations;

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
                    cfg.CreateMap<CreateUpdateGroupDTO, Group>();
                }).CreateMapper(),
                new Mock<IUserRepository>().Object,
                new Mock<IBusinessHourRepository>().Object               
            );
            _controller = new GroupsController(groupService);
        }

        //[Fact]
        //public async Task CreateGroup_ValidInput_ReturnsOk()
        //{
        //    var createUpdateGroupDTO = new CreateUpdateGroupDTO
        //    {
        //        GroupName = "GroupName",
        //        Description = "Description",
        //        GroupLeader = "USER000001",
        //    };

        //    var result = await _controller.CreateGroup(createUpdateGroupDTO);

        //    Assert.IsType<OkObjectResult>(result);
        //}

        [Fact]
        public async Task CreateGroup_MissingGroupName_ReturnsOk()
        {
            var createUpdateGroupDTO = new CreateUpdateGroupDTO
            {
                Description = "Description",
                GroupLeader = "USER000001",
            };

            var context = new ValidationContext(createUpdateGroupDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateGroupDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The GroupName field is required.");
        }

        [Fact]
        public async Task CreateGroup_InvalidGroupName_ReturnsOk()
        {
            var createUpdateGroupDTO = new CreateUpdateGroupDTO
            {
                GroupName = new string('a', 101),
                Description = "Description",
                GroupLeader = "USER000001",
            };

            var context = new ValidationContext(createUpdateGroupDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateGroupDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field GroupName must be a string or array type with a maximum length of '100'.");
        }

        [Fact]
        public async Task CreateGroup_InvalidDescription_ReturnsOk()
        {
            var createUpdateGroupDTO = new CreateUpdateGroupDTO
            {
                GroupName = "GroupName",
                Description = new string('a', 256),
                GroupLeader = "USER000001",
            };

            var context = new ValidationContext(createUpdateGroupDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateGroupDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field Description must be a string or array type with a maximum length of '255'.");
        }

        [Fact]
        public async Task CreateGroup_MissingGroupLeader_ReturnsOk()
        {
            var createUpdateGroupDTO = new CreateUpdateGroupDTO
            {
                GroupName = "GroupName",
                Description = "Description"
            };

            var context = new ValidationContext(createUpdateGroupDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateGroupDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The GroupLeader field is required.");
        }

        [Fact]
        public async Task CreateGroup_InvalidGroupLeader_ReturnsOk()
        {
            var createUpdateGroupDTO = new CreateUpdateGroupDTO
            {
                GroupName = "GroupName",
                Description = "Description",
                GroupLeader = new string('a', 11),
            };

            var context = new ValidationContext(createUpdateGroupDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateGroupDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field GroupLeader must be a string or array type with a maximum length of '10'.");
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
