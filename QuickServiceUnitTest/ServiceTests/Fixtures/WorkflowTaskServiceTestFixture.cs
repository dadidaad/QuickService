using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests.Fixtures
{
    public class WorkflowTaskServiceTestFixture
    {
        public Mock<IWorkflowTaskRepository> MockWorkflowTaskRepository { get; private set; }
        public Mock<IWorkflowRepository> MockWorkflowRepository { get; private set; }
        public Mock<IWorkflowAssignmentRepository> MockWorkflowAssignmentRepository { get; private set; }
        public Mock<IWorkflowAssignmentService> MockWorkflowAssignmentService { get; private set; }
        public Mock<IMapper> MockMapper { get; private set; }
        public Mock<IUserRepository> MockUserRepository { get; private set; }
        public Mock<IGroupRepository> MockGroupRepository { get; private set; }
        public WorkflowTaskService WorkflowTaskService { get; private set; }

        public WorkflowTaskServiceTestFixture()
        {
            MockWorkflowTaskRepository = new Mock<IWorkflowTaskRepository>();
            MockWorkflowRepository = new Mock<IWorkflowRepository>();
            MockWorkflowAssignmentRepository = new Mock<IWorkflowAssignmentRepository>();
            MockWorkflowAssignmentService = new Mock<IWorkflowAssignmentService>();
            MockMapper = new Mock<IMapper>();
            MockUserRepository = new Mock<IUserRepository>();
            MockGroupRepository = new Mock<IGroupRepository>();

            WorkflowTaskService = new WorkflowTaskService(
                MockWorkflowTaskRepository.Object,
                MockWorkflowRepository.Object,
                MockMapper.Object,
                MockUserRepository.Object,
                MockGroupRepository.Object,
                MockWorkflowAssignmentRepository.Object,
                MockWorkflowAssignmentService.Object
            );
        }
    }
    [CollectionDefinition("WorkflowTaskServiceTest")]
    public class WorkflowTaskCollection : ICollectionFixture<WorkflowTaskServiceTestFixture>
    {
    }
}
