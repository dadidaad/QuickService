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
                }).CreateMapper(),
            new Mock<IPermissionRepository>().Object
            );
            _controller = new RolesController(roleService);
        }

        [Fact]
        public void GetAllRoles_ReturnsOkResult()
        {
            var result = _controller.GetAllRoles();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<RoleDTO>;

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<RoleDTO>>(resultType.Value);
            Assert.Equal(8, resultList.Count);
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
