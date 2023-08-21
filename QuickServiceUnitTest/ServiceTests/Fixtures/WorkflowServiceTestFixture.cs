using AutoMapper;
using Moq;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests.Fixtures
{
    public class WorkflowServiceTestFixture
    {
        public Mock<IWorkflowRepository> MockWorkflowRepository { get; private set; }
        public Mock<IUserRepository> MockUserRepository { get; private set; }
        public Mock<IMapper> MockMapper { get; private set; }
        public Mock<IServiceItemRepository> MockServiceItemRepository { get; private set; }
        public Mock<ISlaRepository> MockSlaRepository { get; private set; }
        public Mock<IWorkflowTaskService> MockWorkflowTaskService { get; private set; }      
        public Mock<IWorkflowAssignmentRepository> MockWorkflowAssignmentRepository { get; private set; }
        public Mock<IRequestTicketRepository> MockRequestTicketRepository { get; private set; }
        public WorkflowService WorkflowService { get; private set; }

        public WorkflowServiceTestFixture() 
        {
            MockWorkflowRepository = new Mock<IWorkflowRepository>();
            MockUserRepository =  new Mock<IUserRepository>();
            MockMapper = new Mock<IMapper>();
            MockServiceItemRepository = new Mock<IServiceItemRepository>();
            MockSlaRepository = new Mock<ISlaRepository>();
            MockWorkflowTaskService = new Mock<IWorkflowTaskService>();
            MockWorkflowAssignmentRepository = new Mock<IWorkflowAssignmentRepository>();
            MockRequestTicketRepository = new Mock<IRequestTicketRepository>();
            WorkflowService = new WorkflowService(
                MockWorkflowRepository.Object,
                MockUserRepository.Object,
                MockMapper.Object,
                MockServiceItemRepository.Object,
                MockSlaRepository.Object,
                MockWorkflowTaskService.Object,
                MockWorkflowAssignmentRepository.Object,
                MockRequestTicketRepository.Object);
        }

        [CollectionDefinition("WorkflowServiceTest")]
        public class WorkflowCollection : ICollectionFixture<WorkflowServiceTestFixture>
        {
        }
    }
}
