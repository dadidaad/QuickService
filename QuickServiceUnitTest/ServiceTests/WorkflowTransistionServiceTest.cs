using Moq;
using QuickServiceUnitTest.ServiceTests.Fixtures;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.DTOs.WorkflowTransition;
using QuickServiceWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests
{
    public class WorkflowTransistionServiceTest
    {
        private readonly TransitionServiceTestFixture _fixture;

        public WorkflowTransistionServiceTest()
        {
            _fixture = new TransitionServiceTestFixture();
        }
        [Fact]
        public async Task CreateWorkflowTransition_ValidInput_Success()
        {
            // Arrange
            var service = _fixture.WorkflowTransitionService;

            var create = new CreateWorkflowTransitionDTO
            {
                FromWorkflowTask = "WOST000003",
                ToWorkflowTask = "WOST000004",
                WorkflowTransitionName = "Transition",
                Condition = true
            };

            _fixture.MockMapper.Setup(mapper => mapper.Map<WorkflowTransition>(It.IsAny<CreateWorkflowTransitionDTO>())).Returns(new WorkflowTransition());
            _fixture.MockMapper.Setup(mapper => mapper.Map<WorkflowTransition>(create)).Returns(new WorkflowTransition());

            _fixture.MockMapper.Setup(mapper => mapper.Map<WorkflowTransition>(It.IsAny<CreateWorkflowTransitionDTO>())).Returns(new WorkflowTransition());


            // Act
            await service.CreateWorkflowTransition(create);

            // Assert
            _fixture.MockWorkflowTransitionRepository.Verify(repo => repo.AddWorkflowTransition(It.IsAny<WorkflowTransition>()), Times.Once);
        }
        [Fact]
        public async Task CreateWorkflowTransition_InvalidInput_Success()
        {
            // Arrange
            var service = _fixture.WorkflowTransitionService;

            var create = new CreateWorkflowTransitionDTO
            {
                FromWorkflowTask = "",
                ToWorkflowTask = "WOST000006",
                WorkflowTransitionName = "Transition",
                Condition = true
            };

            _fixture.MockMapper.Setup(mapper => mapper.Map<WorkflowTransition>(It.IsAny<CreateWorkflowTransitionDTO>())).Returns(new WorkflowTransition());
            _fixture.MockMapper.Setup(mapper => mapper.Map<WorkflowTransition>(create)).Returns(new WorkflowTransition());

            _fixture.MockMapper.Setup(mapper => mapper.Map<WorkflowTransition>(It.IsAny<CreateWorkflowTransitionDTO>())).Returns(new WorkflowTransition());


            // Act
            await service.CreateWorkflowTransition(create);

            // Assert
            _fixture.MockWorkflowTransitionRepository.Verify(repo => repo.AddWorkflowTransition(It.IsAny<WorkflowTransition>()), Times.Once);
        }
    }
}
