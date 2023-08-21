using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests.Fixtures
{
    public class TransitionServiceTestFixture
    {
        public Mock<IWorkflowTransitionRepository> MockWorkflowTransitionRepository { get; private set; }
        public Mock<ILogger<RoleService>> MockLogger { get; private set; }
        public Mock<IJWTUtils> MockJWTUtils { get; private set; }
        public Mock<IMapper> MockMapper { get; private set; }
        public Mock<IOptions<AzureStorageConfig>> MockStorageConfig { get; private set; }
        public WorkflowTransitionService WorkflowTransitionService { get; private set; }
        public Mock<IWorkflowTaskRepository> MockIIWorkflowTaskRepository { get; private set; }
        public Mock<IWorkflowRepository> MockIWorkflowRepository { get; private set; }

        public TransitionServiceTestFixture()
        {
            MockWorkflowTransitionRepository = new Mock<IWorkflowTransitionRepository>();
            MockMapper = new Mock<IMapper>();
            MockIIWorkflowTaskRepository = new Mock<IWorkflowTaskRepository>();
            MockIWorkflowRepository = new Mock<IWorkflowRepository>();

            WorkflowTransitionService = new WorkflowTransitionService(
               MockWorkflowTransitionRepository.Object,
               MockIIWorkflowTaskRepository.Object,
               MockMapper.Object,
               MockIWorkflowRepository.Object
            );
        }
    }
}
