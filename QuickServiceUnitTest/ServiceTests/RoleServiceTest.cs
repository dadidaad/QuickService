using Moq;
using QuickServiceUnitTest.ServiceTests.DerivedClasses;
using QuickServiceUnitTest.ServiceTests.Fixtures;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.Role;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests
{
    [Collection("RoleServiceTest")]

    public class RoleServiceTest
    {
        private readonly RoleServiceTestFixture _fixture;

        public RoleServiceTest()
        {
            _fixture = new RoleServiceTestFixture();
        }
        [Fact]
        public async Task CreateRole_ValidInput_Success()
        {
            // Arrange
            var roleService = _fixture.RoleService;
            List<Permission> permissions = new List<Permission>()
            {
               new ()
                {
                    PermissionId = "PERM000002",
                    PermissionName = "Manage tickets",
                },
                new()
                {
                    PermissionId = "PERM0000020",
                    PermissionName = "Manage changes",
                },
                new()
                {
                    PermissionId = "PERM0000021",
                    PermissionName = "Manage problems",
                }
            };
            var createDTO = new CreateDTO
            {
                RoleName = "Role Name Test Valid Input",
                Description = "Role Description Test Valid Input",
                RoleType = 0
            };
            _fixture.MockMapper.Setup(mapper => mapper.Map<Role>(It.IsAny<CreateDTO>())).Returns(new Role());
            _fixture.MockMapper.Setup(mapper => mapper.Map<Role>(createDTO)).Returns(new Role());
            _fixture.MockMapper.Setup(mapper => mapper.Map<Role>(It.IsAny<CreateDTO>())).Returns(new Role { Permissions = permissions });

            // Act
            await roleService.CreateRole(createDTO);

            // Assert
            _fixture.MockRoleRepository.Verify(repo => repo.CreateRole(It.IsAny<Role>()), Times.Once);
        }

        [Fact]
        public async Task UpdateRole_ValidData_UpdatesUserSuccessfully()
        {

            var testableRoleService = new TestableRoleService(
                _fixture.MockRoleRepository.Object,
                _fixture.MockMapper.Object,
                _fixture.MockPermissionRepository.Object
            );

            var updateDTO = new UpdateDTO
            {
                RoleId = "ROLE1",
                // Other properties
            };

            var existingRole = new Role
            { 
                RoleId = "USER1",
                // Other properties
            };

            _fixture.MockRoleRepository.Setup(repo => repo.GetRoleById(It.IsAny<string>()))
                .ReturnsAsync(existingRole);

            _fixture.MockMapper.Setup(map => map.Map(It.IsAny<UpdateDTO>(), It.IsAny<Role>()))
                .Returns(existingRole);
            // Act
            await testableRoleService.UpdateRole(updateDTO);

            // Assert
            _fixture.MockRoleRepository.Verify(repo => repo.UpdateRole(existingRole), Times.Once);
        }
        [Fact]
        public async Task UpdateRole_RoleNotFound_ThrowsAppException()
        {

            var userService = _fixture.RoleService;

            var updateDTO = new UpdateDTO
            {
                RoleId = "ROLE1",
                // Other properties
            };

            _fixture.MockRoleRepository.Setup(repo => repo.GetRoleById(It.IsAny<string>()))
                .ReturnsAsync((Role)null); 

            // Act and Assert
            await Assert.ThrowsAsync<AppException>(async () => await userService.UpdateRole(updateDTO));
        }
    }
}
