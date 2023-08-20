using Moq;
using QuickServiceUnitTest.ServiceTests.Fixtures;
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
