using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickServiceWebAPI.DTOs.Role;
using QuickServiceWebAPI.DTOs.Permission;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.Utilities;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.DTOs.Group;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceUnitTest
{
    public class RolesControllerTest
    {
        private readonly RolesController _controller;
        public RolesControllerTest()
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<RoleRepository>>();

            var roleService = new RoleService(
                new RoleRepository(dbContext, mockLogger.Object),
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Role, RoleDTO>();
                    cfg.CreateMap<Permission, PermissionDTO>();
                    cfg.CreateMap<QuickServiceWebAPI.DTOs.Role.UpdateDTO, Role>();
                }).CreateMapper(),
            new Mock<IPermissionRepository>().Object
            );
            _controller = new RolesController(roleService);
        }

        //[Fact]
        //public async Task UpdateRole_ValidInput_ReturnsOk()
        //{
        //    var updateRoleDTO = new QuickServiceWebAPI.DTOs.Role.UpdateDTO
        //    {
        //        RoleId = "ROLE000011",
        //        RoleName = "RoleName",
        //        Description = "Description",
        //        RoleType = (int)RoleType.Admin
        //};

        //    var result = await _controller.UpdateRole(updateRoleDTO);

        //    Assert.IsType<OkObjectResult>(result);
        //}

        [Fact]
        public async Task UpdateRole_MissingRoleId_ReturnsOk()
        {
            var updateRoleDTO = new QuickServiceWebAPI.DTOs.Role.UpdateDTO
            {
                RoleName = "RoleName",
                Description = "Description"
            };

            var context = new ValidationContext(updateRoleDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(updateRoleDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The RoleId field is required.");
        }

        [Fact]
        public async Task UpdateRole_InvalidRoleId_ReturnsOk()
        {
            var updateRoleDTO = new QuickServiceWebAPI.DTOs.Role.UpdateDTO
            {
                RoleId = new string('a', 11),
                RoleName = "RoleName",
                Description = "Description"
            };

            var context = new ValidationContext(updateRoleDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(updateRoleDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field RoleId must be a string or array type with a maximum length of '10'.");
        }

        [Fact]
        public async Task UpdateRole_MissingRoleName_ReturnsOk()
        {
            var updateRoleDTO = new QuickServiceWebAPI.DTOs.Role.UpdateDTO
            {
                RoleId = "ROLE000011",
                Description = "Description"
            };

            var context = new ValidationContext(updateRoleDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(updateRoleDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The RoleName field is required.");
        }

        [Fact]
        public async Task UpdateRole_InvalidRoleName_ReturnsOk()
        {
            var updateRoleDTO = new QuickServiceWebAPI.DTOs.Role.UpdateDTO
            {
                RoleId = "ROLE000011",
                RoleName = new string('a', 51),
                Description = "Description"
            };

            var context = new ValidationContext(updateRoleDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(updateRoleDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field RoleName must be a string or array type with a maximum length of '50'.");
        }

        [Fact]
        public async Task UpdateRole_InvalidDescription_ReturnsOk()
        {
            var updateRoleDTO = new QuickServiceWebAPI.DTOs.Role.UpdateDTO
            {
                RoleId = "ROLE000011",
                RoleName = "RoleName",
                Description = new string('a', 257)
            };

            var context = new ValidationContext(updateRoleDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(updateRoleDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field Description must be a string or array type with a maximum length of '255'.");
        }


        [Fact]
        public async Task GetRole_ReturnsOkResult()
        {
            //Arrange
            string valid_roleId = "ROLE000001";
            //string inValid_roleId = "ABCDA123";

            //Act
            //var errorResult = await _controller.GetRole(inValid_roleId);
            var successResult = await _controller.GetRole(valid_roleId);

            var successModel = successResult as OkObjectResult;
            var fetchedRole = successModel.Value as RoleDTO;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<AppException>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<RoleDTO>(fetchedRole);
        }
    }
}
