using Moq;
using QuickServiceUnitTest.ServiceTests.Fixtures;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests
{
    [CollectionDefinition("WorkflowServiceTest")]
    public class WorkflowServiceTest
    {
        private readonly WorkflowServiceTestFixture _fixture;
        public WorkflowServiceTest() 
        { 
            _fixture = new WorkflowServiceTestFixture();
        }

        [Fact]
        public async Task CreateWorkflow_ValidInput_Success()
        {
            // Arrange
            var workflowService = _fixture.WorkflowService;

            var creator = new User
            {
                UserId = "USER1"
            };

            var createUpdateWorkflowDTO = new CreateUpdateWorkflowDTO
            {
               CreatedBy = creator.UserId
            };


            _fixture.MockUserRepository.Setup(repo => repo.GetUserDetails(It.IsAny<string>())).ReturnsAsync((User?)creator);
            _fixture.MockMapper.Setup(mapper => mapper.Map<Workflow>(It.IsAny<CreateUpdateWorkflowDTO>())).Returns(new Workflow());
            _fixture.MockMapper.Setup(mapper => mapper.Map<Workflow>(createUpdateWorkflowDTO)).Returns(new Workflow());

            // Act
            await workflowService.CreateWorkflow(createUpdateWorkflowDTO);

            // Assert
            _fixture.MockWorkflowRepository.Verify(repo => repo.AddWorkflow(It.IsAny<Workflow>()), Times.Once);
        }

        [Fact]
        public async Task CreateWorkflow_InvalidUser_ThrowsException()
        {

            // Arrange
            var workflowService = _fixture.WorkflowService;

            var creator = new User
            {
                UserId = "USER1"
            };

            var createUpdateWorkflowDTO = new CreateUpdateWorkflowDTO
            {
                CreatedBy = creator.UserId
            };

            _fixture.MockUserRepository.Setup(repo => repo.GetUserDetails(createUpdateWorkflowDTO.CreatedBy)).ReturnsAsync((User?)null);

            // Act & Assert
            await Assert.ThrowsAsync<AppException>(async () => await workflowService.CreateWorkflow(createUpdateWorkflowDTO));
        }

        [Fact]
        public async Task UpdateWorkflow_WorkflowNotFound_ThrowsAppException()
        {

            // Arrange
            var workflowService = _fixture.WorkflowService;

            var workflow = new Workflow
            {
                WorkflowId = "WORK1"
            };

            var creator = new User
            {
                UserId = "USER1"
            };

            var createUpdateWorkflowDTO = new CreateUpdateWorkflowDTO
            {
                CreatedBy = creator.UserId
            };
            _fixture.MockWorkflowRepository.Setup(repo => repo.GetWorkflowById(workflow.WorkflowId))
                .ReturnsAsync((Workflow?)null);
            _fixture.MockUserRepository.Setup(repo => repo.GetUserDetails(createUpdateWorkflowDTO.CreatedBy)).ReturnsAsync((User?)creator);

            // Act and Assert
            await Assert.ThrowsAsync<AppException>(async () => await workflowService.UpdateWorkflow(workflow.WorkflowId, createUpdateWorkflowDTO));
        }

        [Fact]
        public async Task UpdateWorkflow_UserNotFound_ThrowsAppException()
        {

            // Arrange
            var workflowService = _fixture.WorkflowService;

            var workflow = new Workflow
            {
                WorkflowId = "WORK1"
            };

            var creator = new User
            {
                UserId = "USER1"
            };

            var createUpdateWorkflowDTO = new CreateUpdateWorkflowDTO
            {
                CreatedBy = creator.UserId
            };
            _fixture.MockWorkflowRepository.Setup(repo => repo.GetWorkflowById(workflow.WorkflowId))
                .ReturnsAsync((Workflow?)workflow);
            _fixture.MockUserRepository.Setup(repo => repo.GetUserDetails(createUpdateWorkflowDTO.CreatedBy)).ReturnsAsync((User?)null);

            // Act and Assert
            await Assert.ThrowsAsync<AppException>(async () => await workflowService.UpdateWorkflow(workflow.WorkflowId, createUpdateWorkflowDTO));
        }
    }
}
