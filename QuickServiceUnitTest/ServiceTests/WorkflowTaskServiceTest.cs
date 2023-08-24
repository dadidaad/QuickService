using Moq;
using QuickServiceUnitTest.ServiceTests.Fixtures;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.WorkflowTask;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests
{
    public class WorkflowTaskServiceTest
    {
        private readonly WorkflowTaskServiceTestFixture _fixture;

        public WorkflowTaskServiceTest()
        {
            _fixture = new WorkflowTaskServiceTestFixture();
        }

        [Fact]
        public async Task CreateWorkflowTask_Valid_Success()
        {
            // Arrange
            var workflowTaskService = _fixture.WorkflowTaskService;

            var workflow = new Workflow
            {
                WorkflowId = "WOFL1"
            };

            var user = new User
            {
                UserId = "USER1"
            };

            var group = new Group
            {
                GroupId = "GROU1"
            };

            var createUpdateWorkflowTaskDTO = new CreateUpdateWorkflowTaskDTO()
            {
                WorkflowTaskName = "abc",
                Status = "Open",
                WorkflowId = workflow.WorkflowId,
                AssignerId = user.UserId       
            };
            bool AcceptResovledAndOpenTask = true;
            
            _fixture.MockUserRepository.Setup(repo => repo.GetUserDetails(It.IsAny<string>())).ReturnsAsync((User?)user);
            _fixture.MockGroupRepository.Setup(repo => repo.GetGroupById(It.IsAny<string>())).ReturnsAsync((Group?)group);
            _fixture.MockWorkflowRepository.Setup(repo => repo.GetWorkflowById(It.IsAny<string>())).ReturnsAsync((Workflow?)workflow);
            _fixture.MockMapper.Setup(mapper => mapper.Map<WorkflowTask>(It.IsAny<CreateUpdateWorkflowTaskDTO>())).Returns(new WorkflowTask());
            _fixture.MockMapper.Setup(mapper => mapper.Map<WorkflowTask>(createUpdateWorkflowTaskDTO)).Returns(new WorkflowTask { });
            // Act
            await workflowTaskService.CreateWorkflowTask(createUpdateWorkflowTaskDTO, AcceptResovledAndOpenTask);

            // Assert
            _fixture.MockWorkflowTaskRepository.Verify(repo => repo.AddWorkflowTask(It.IsAny<WorkflowTask>()), Times.Once);
        }

        [Fact]
        public async Task CreateWorkflowTask_InvalidWorkflow_ThrowsException()
        {

            // Arrange
            var workflowTaskService = _fixture.WorkflowTaskService;

            var workflow = new Workflow
            {
                WorkflowId = "WOFL1"
            };

            var user = new User
            {
                UserId = "USER1"
            };

            var group = new Group
            {
                GroupId = "GROU1"
            };

            var createUpdateWorkflowTaskDTO = new CreateUpdateWorkflowTaskDTO()
            {
                WorkflowTaskName = "abc",
                Status = "Open",
                WorkflowId = workflow.WorkflowId,
                AssignerId = user.UserId
            };
            bool AcceptResovledAndOpenTask = true;

            _fixture.MockWorkflowRepository.Setup(repo => repo.GetWorkflowById(workflow.WorkflowId)).ReturnsAsync((Workflow?)null);

            // Act & Assert
            await Assert.ThrowsAsync<AppException>(async () => await workflowTaskService.CreateWorkflowTask(createUpdateWorkflowTaskDTO, AcceptResovledAndOpenTask));
        }

        [Fact]
        public async Task CreateWorkflowTask_NotAcceptResovledAndOpenTask_ThrowsException()
        {

            // Arrange
            var workflowTaskService = _fixture.WorkflowTaskService;

            var workflow = new Workflow
            {
                WorkflowId = "WOFL1"
            };

            var user = new User
            {
                UserId = "USER1"
            };

            var group = new Group
            {
                GroupId = "GROU1"
            };

            var createUpdateWorkflowTaskDTO = new CreateUpdateWorkflowTaskDTO()
            {
                WorkflowTaskName = "abc",
                Status = "Open",
                WorkflowId = workflow.WorkflowId,
                AssignerId = user.UserId
            };
            bool AcceptResovledAndOpenTask = false;

            // Act & Assert
            await Assert.ThrowsAsync<AppException>(async () => await workflowTaskService.CreateWorkflowTask(createUpdateWorkflowTaskDTO, AcceptResovledAndOpenTask));
        }

        [Fact]
        public async Task CreateWorkflowTask_InvalidAssignee_ThrowsException()
        {

            // Arrange
            var workflowTaskService = _fixture.WorkflowTaskService;

            var workflow = new Workflow
            {
                WorkflowId = "WOFL1"
            };

            var user = new User
            {
                UserId = "USER1"
            };

            var group = new Group
            {
                GroupId = "GROU1"
            };

            var createUpdateWorkflowTaskDTO = new CreateUpdateWorkflowTaskDTO()
            {
                WorkflowTaskName = "abc",
                Status = "Open",
                WorkflowId = workflow.WorkflowId,
                AssignerId = user.UserId
            };
            bool AcceptResovledAndOpenTask = true;

            _fixture.MockUserRepository.Setup(repo => repo.GetUserDetails(It.IsAny<string>())).ReturnsAsync((User?)null);
            // Act & Assert
            await Assert.ThrowsAsync<AppException>(async () => await workflowTaskService.CreateWorkflowTask(createUpdateWorkflowTaskDTO, AcceptResovledAndOpenTask));
        }

        [Fact]
        public async Task CreateWorkflowTask_InvalidGroup_ThrowsException()
        {

            // Arrange
            var workflowTaskService = _fixture.WorkflowTaskService;

            var workflow = new Workflow
            {
                WorkflowId = "WOFL1"
            };

            var user = new User
            {
                UserId = "USER1"
            };

            var group = new Group
            {
                GroupId = "GROU1"
            };

            var createUpdateWorkflowTaskDTO = new CreateUpdateWorkflowTaskDTO()
            {
                WorkflowTaskName = "abc",
                Status = "Open",
                WorkflowId = workflow.WorkflowId,
                AssignerId = user.UserId
            };
            bool AcceptResovledAndOpenTask = true;

            _fixture.MockGroupRepository.Setup(repo => repo.GetGroupById(It.IsAny<string>())).ReturnsAsync((Group?)null);
            // Act & Assert
            await Assert.ThrowsAsync<AppException>(async () => await workflowTaskService.CreateWorkflowTask(createUpdateWorkflowTaskDTO, AcceptResovledAndOpenTask));
        }

        [Fact]
        public async Task DeleteWorkflowTask_ValidId_DeletesTaskAndAssignments()
        {
            // Arrange
            var workflowTaskId = "valid_id";
            var workflowTask = new WorkflowTask
            {
                // Initialize properties
            };
            var listWorkflowAssignment = new List<WorkflowAssignment>
            {
                new WorkflowAssignment(),
                new WorkflowAssignment()
            };

            _fixture.MockWorkflowTaskRepository.Setup(repo => repo.GetWorkflowTaskById(workflowTaskId))
                .ReturnsAsync(workflowTask);

            _fixture.MockWorkflowAssignmentRepository.Setup(repo => repo.GetWorkflowAssignmentsByWorkflowTaskId(workflowTaskId))
                .ReturnsAsync(listWorkflowAssignment);

            // Act
            await _fixture.WorkflowTaskService.DeleteWorkflowTask(workflowTaskId);

            // Assert
            _fixture.MockWorkflowAssignmentService.Verify(
                service => service.DeleteListWorkflowAssignment(listWorkflowAssignment), Times.Once);
            _fixture.MockWorkflowTaskRepository.Verify(repo => repo.DeleteWorkflowTask(workflowTask), Times.Once);
        }

        [Fact]
        public async Task DeleteWorkflowTask_InvalidId_ThrowsAppException()
        {
            // Arrange
            var workflowTaskId = "invalid_id";

            _fixture.MockWorkflowTaskRepository.Setup(repo => repo.GetWorkflowTaskById(workflowTaskId))
                .ReturnsAsync((WorkflowTask)null);

            // Act & Assert
            await Assert.ThrowsAsync<AppException>(
                () => _fixture.WorkflowTaskService.DeleteWorkflowTask(workflowTaskId));
        }

        [Fact]
        public async Task DeleteWorkflowTask_ValidIdWithNoAssignments_DeletesTaskOnly()
        {
            // Arrange
            var workflowTaskId = "valid_id";
            var workflowTask = new WorkflowTask
            {
                // Initialize properties
            };

            _fixture.MockWorkflowTaskRepository.Setup(repo => repo.GetWorkflowTaskById(workflowTaskId))
                .ReturnsAsync(workflowTask);

            _fixture.MockWorkflowAssignmentRepository.Setup(repo => repo.GetWorkflowAssignmentsByWorkflowTaskId(workflowTaskId))
                .ReturnsAsync(new List<WorkflowAssignment>());

            // Act
            await _fixture.WorkflowTaskService.DeleteWorkflowTask(workflowTaskId);

            // Assert
            _fixture.MockWorkflowAssignmentService.Verify(
                service => service.DeleteListWorkflowAssignment(It.IsAny<List<WorkflowAssignment>>()), Times.Never);
            _fixture.MockWorkflowTaskRepository.Verify(repo => repo.DeleteWorkflowTask(workflowTask), Times.Once);
        }
    }
}
