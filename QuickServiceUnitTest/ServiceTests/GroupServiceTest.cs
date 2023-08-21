using Moq;
using QuickServiceUnitTest.ServiceTests.DerivedClasses;
using QuickServiceUnitTest.ServiceTests.Fixtures;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.Role;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests
{
    public class GroupServiceTest
    {
        private readonly GroupServiceTestFixture _fixture;

        public GroupServiceTest()
        {
            _fixture = new GroupServiceTestFixture();
        }
        [Fact]
        public async Task CreateGroup_ValidInput_Success()
        {
            // Arrange
            var groupService = _fixture.GroupService;

            var createUpdateGroupDTO = new CreateUpdateGroupDTO
            {
                GroupName = "Group Name Test Valid Input",
                Description = "Role Description Test Valid Input",
                GroupLeader = "USER0001",
            };

            _fixture.MockMapper.Setup(mapper => mapper.Map<Group>(It.IsAny<CreateUpdateGroupDTO>())).Returns(new Group());
            _fixture.MockMapper.Setup(mapper => mapper.Map<Group>(createUpdateGroupDTO)).Returns(new Group());
            _fixture.MockMapper.Setup(mapper => mapper.Map<Group>(It.IsAny<CreateUpdateGroupDTO>())).Returns(new Group { NeedApprovalByLeader = false, IsRestricted = false });


            // Act
            await groupService.CreateGroup(createUpdateGroupDTO);

            // Assert
            _fixture.MockGroupRepository.Verify(repo => repo.AddGroup(It.IsAny<Group>()), Times.Once);
        }
        [Fact]
        public async Task UpdateGroup_GroupIdNotFound_UpdatesGroupSuccessfully()
        {

            var testableGroupService = new TestableGroupService(
                _fixture.MockGroupRepository.Object,
                _fixture.MockMapper.Object,
                _fixture.MockUserRepository.Object,
                _fixture.MockBusinessHourRepository.Object
            );
            var groupService = _fixture.GroupService;

            var createUpdateGroupDTO = new CreateUpdateGroupDTO
            {
                GroupName = "GroupName",
                Description = "Description",
                GroupLeader = "USER0001",
                // Other properties
            };

            var groupId = "GROU000001";
            _fixture.MockUserRepository.Setup(repo => repo.GetUserDetails(It.IsAny<string>()))
                .ReturnsAsync((User)null);
            // Act
            await Assert.ThrowsAsync<AppException>(async () => await groupService.UpdateGroup(groupId, createUpdateGroupDTO));

        }
        [Fact]
        public async Task UpdateGroup_GroupNotFound_ThrowsAppException()
        {

            var groupService = _fixture.GroupService;

            var updateDTO = new CreateUpdateGroupDTO
            {
                GroupName = "GroupNameUpdate",
                Description = "DescriptionUpdate",
                GroupLeader = "USER0001",
                // Other properties
            };
            string groupId = "GROU000000";

            _fixture.MockGroupRepository.Setup(repo => repo.GetGroupById(It.IsAny<string>()))
                .ReturnsAsync((Group)null);

            // Act and Assert
            await Assert.ThrowsAsync<AppException>(async () => await groupService.UpdateGroup(groupId, updateDTO));
        }

        [Fact]
        public async Task UpdateGroup_ValidData_UpdatesGroupSuccessfully()
        {

            var testableGroupService = new TestableGroupService(
                _fixture.MockGroupRepository.Object,
                _fixture.MockMapper.Object,
                _fixture.MockUserRepository.Object,
                _fixture.MockBusinessHourRepository.Object
            );
            var groupService = _fixture.GroupService;

            var createUpdateGroupDTO = new CreateUpdateGroupDTO
            {
                GroupName = "GroupName",
                Description = "Description",
                GroupLeader = "USER0001",
                // Other properties
            };

            var existingGroup = new Group
            {
                GroupId = "GROU000001",
                GroupName = "GroupNameUpdate",
                Description = "DescriptionUpdate",
                GroupLeader = "USER0002",
                // Other properties
            };
            var existingUser = new User
            {
                UserId = "USER00001"
            };
            var groupId = "GROU000001";
            _fixture.MockGroupRepository.Setup(repo => repo.GetGroupById(It.IsAny<string>()))
                .ReturnsAsync(existingGroup);
            _fixture.MockUserRepository.Setup(repo => repo.GetUserDetails(It.IsAny<string>()))
                .ReturnsAsync(existingUser);
            _fixture.MockMapper.Setup(map => map.Map(It.IsAny<CreateUpdateGroupDTO>(), It.IsAny<Group>()))
                .Returns(existingGroup);

            // Act
            await testableGroupService.UpdateGroup(groupId, createUpdateGroupDTO);
            //Assert
            _fixture.MockGroupRepository.Verify(repo => repo.UpdateGroup(existingGroup), Times.Once);

        }

        [Fact]
        public async Task GetGroupById_ValidData_ReturnOks()
        {
            var testableGroupService = new TestableGroupService(
                _fixture.MockGroupRepository.Object,
                _fixture.MockMapper.Object,
                _fixture.MockUserRepository.Object,
                _fixture.MockBusinessHourRepository.Object
            );
            var groupService = _fixture.GroupService;
            var existingGroup = new Group
            {
                GroupId = "GROU000001",
                GroupName = "GroupName",
                Description = "Description",
                GroupLeader = "USER0002",
                // Other properties
            };
            string groupId = "GROU000001";
            _fixture.MockGroupRepository.Setup(repo => repo.GetGroupById(It.IsAny<string>()))
                .ReturnsAsync(existingGroup);
            // Act
            await testableGroupService.GetGroupById(groupId);
            // Assert
            _fixture.MockGroupRepository.Verify(repo => repo.GetGroupById(groupId), Times.Once);
        }

        [Fact]
        public async Task GetGroupById_Invalid_ThrowsAppException()
        {
            var testableGroupService = new TestableGroupService(
                _fixture.MockGroupRepository.Object,
                _fixture.MockMapper.Object,
                _fixture.MockUserRepository.Object,
                _fixture.MockBusinessHourRepository.Object
            );
            var groupService = _fixture.GroupService;
            
            string groupId = "GROU0000000";

            _fixture.MockGroupRepository.Setup(repo => repo.GetGroupById(It.IsAny<string>()))
                .ReturnsAsync((Group)null);
            // Assert
            await Assert.ThrowsAsync<AppException>(async () => await groupService.GetGroupById(groupId));

        }
    }
}
