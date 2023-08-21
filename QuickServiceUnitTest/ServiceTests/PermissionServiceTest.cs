using Moq;
using QuickServiceUnitTest.ServiceTests.DerivedClasses;
using QuickServiceUnitTest.ServiceTests.Fixtures;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests
{
    public class PermissionServiceTest
    {
        private readonly PermissionServiceTestFixture _fixture;

        public PermissionServiceTest()
        {
            _fixture = new PermissionServiceTestFixture();
        }
        [Fact]
        public async Task GetPermissionByRole_ValidData_ReturnOk()
        {

            var service = _fixture.PermissionService;

            var existingRole = new Role
            {
                RoleId = "ROLE000001",
                // Other properties
            };

            string roleId = "ROLE000001";

            _fixture.MockRoleRepository.Setup(repo => repo.GetRoleById(It.IsAny<string>()))
                .ReturnsAsync((existingRole));

            // Act
            await service.GetPermissionsByRole(roleId);

            // Assert
            _fixture.MockPermissionRepository.Verify(repo => repo.GetPermissionsByRole(It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public async Task GetPermissionByRole_InvalidData_ThrowAppException()
        {

            var service = _fixture.PermissionService;

            var existingRole = new Role
            {
                RoleId = "ROLE000001",
                // Other properties
            };

            string roleId = "ROLE000001";

            _fixture.MockRoleRepository.Setup(repo => repo.GetRoleById(It.IsAny<string>()))
                .ReturnsAsync((Role)null);

            // Assert
            await Assert.ThrowsAsync<AppException>(async () => await service.GetPermissionsByRole(roleId));
        }
        [Fact]
        public async Task UpdatePermissionsToRole_ValidData_UpdateSuccessfully()
        {

            var testablePermissionService = new TestablePermissionService(
                _fixture.MockPermissionRepository.Object,
                _fixture.MockMapper.Object,
                _fixture.MockRoleRepository.Object
            );
            var service = _fixture.PermissionService;
            List<PermissionDTO> permissions = new List<PermissionDTO>()
                {
                        new PermissionDTO{ PermissionId="ROLE000001", PermissionName="Permission", IsGranted=true },
                };
            var existingRole = new Role
            {
                RoleId = "ROLE000001",
                // Other properties
            };

            var createUpdatePermissionDTO = new UpdatePermissionsDTO
            {
                RoleId = existingRole.RoleId,
                Permissions = permissions,
                // Other properties
            };
            
            
            var exPermission = new Permission
            {
                PermissionId = "PER00001",
                PermissionName = "Permission1"
                               
            };
            var updatePermission = new Permission
            {
                PermissionId = "PER00002",
                PermissionName = "Permission2"               
            };
            var groupId = "GROU000001";
            _fixture.MockRoleRepository.Setup(repo => repo.GetRoleById(It.IsAny<string>()))
                .ReturnsAsync(existingRole);
            _fixture.MockPermissionRepository.Setup(repo => repo.GetPermission(It.IsAny<string>()))
                .ReturnsAsync(exPermission);
            _fixture.MockMapper.Setup(map => map.Map(It.IsAny<UpdatePermissionsDTO>(), It.IsAny<Role>()))
                .Returns(existingRole);

            // Act
            await testablePermissionService.UpdatePermissionsToRole(createUpdatePermissionDTO);
            //Assert
            _fixture.MockRoleRepository.Verify(repo => repo.UpdateRole(existingRole), Times.Once);

        }

        [Fact]
        public async Task UpdatePermissionsToRole_RoleNotFound_ThrowAddException()
        {

            var testablePermissionService = new TestablePermissionService(
                _fixture.MockPermissionRepository.Object,
                _fixture.MockMapper.Object,
                _fixture.MockRoleRepository.Object
            );
            var service = _fixture.PermissionService;
            List<PermissionDTO> permissions = new List<PermissionDTO>()
                {
                        new PermissionDTO{ PermissionId="ROLE000001", PermissionName="Permission", IsGranted=true },
                };
            var createUpdatePermissionDTO = new UpdatePermissionsDTO
            {
                RoleId = "ROLE000001",
                Permissions = permissions,
                // Other properties
            };

            var existingRole = new Role
            {
                RoleId = "ROLE000001",
                // Other properties
            };
            var exPermission = new Permission
            {
                PermissionId = "PER00001",
                PermissionName = "Permission1"
            };
            var updatePermission = new Permission
            {
                PermissionId = "PER00001",
                PermissionName = "Permission2"
            };
            var groupId = "GROU000001";
            _fixture.MockRoleRepository.Setup(repo => repo.GetRoleById(It.IsAny<string>()))
                .ReturnsAsync((Role)null);
            _fixture.MockPermissionRepository.Setup(repo => repo.GetPermission(It.IsAny<string>()))
                .ReturnsAsync(exPermission);
            _fixture.MockMapper.Setup(map => map.Map(It.IsAny<UpdatePermissionsDTO>(), It.IsAny<Role>()))
                .Returns(existingRole);

            await Assert.ThrowsAsync<AppException>(async () => await service.UpdatePermissionsToRole(createUpdatePermissionDTO));
        }
        [Fact]
        public async Task UpdatePermissionsToRole_PermissionNotFound_ThrowAddException()
        {

            var testablePermissionService = new TestablePermissionService(
                _fixture.MockPermissionRepository.Object,
                _fixture.MockMapper.Object,
                _fixture.MockRoleRepository.Object
            );
            var service = _fixture.PermissionService;
            List<PermissionDTO> permissions = new List<PermissionDTO>()
                {
                        new PermissionDTO{ PermissionId="ROLE000001", PermissionName="Permission", IsGranted=true },
                };
            var createUpdatePermissionDTO = new UpdatePermissionsDTO
            {
                RoleId = "ROLE000001",
                Permissions = permissions,
                // Other properties
            };

            var existingRole = new Role
            {
                RoleId = "ROLE000001",
                // Other properties
            };
            var exPermission = new Permission
            {
                PermissionId = "PER00001",
                PermissionName = "Permission1"
            };
            var updatePermission = new Permission
            {
                PermissionId = "PER00001",
                PermissionName = "Permission2"
            };
            var groupId = "GROU000001";
            _fixture.MockRoleRepository.Setup(repo => repo.GetRoleById(It.IsAny<string>()))
                .ReturnsAsync(existingRole);
            _fixture.MockPermissionRepository.Setup(repo => repo.GetPermission(It.IsAny<string>()))
                .ReturnsAsync((Permission)null);
            _fixture.MockMapper.Setup(map => map.Map(It.IsAny<UpdatePermissionsDTO>(), It.IsAny<Role>()))
                .Returns(existingRole);

            await Assert.ThrowsAsync<AppException>(async () => await service.UpdatePermissionsToRole(createUpdatePermissionDTO));
        }
    }
}
