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
    public class RequestTicketServiceTestFixture
    {
        public Mock<IRequestTicketRepository> MockRequestTicketRepository { get; private set; }
        public Mock<ILogger<RequestTicketService>> MockLogger { get; private set; }
        public Mock<IMapper> MockMapper { get; private set; }
        public Mock<IUserRepository> MockUserRepository { get; private set; }
        public Mock<IServiceItemRepository> MockServiceItemRepository { get; private set; }
        public Mock<IAttachmentService> MockAttachmentService { get; private set; }
        public Mock<ISlaRepository> MockSlaRepository { get; private set; }
        public Mock<IWorkflowAssignmentService> MockWorkflowAssignmentService { get; private set; }
        public Mock<IRequestTicketHistoryService> MockRequestTicketHistoryService { get; private set; }
        public Mock<IRequestTicketHistoryRepository> MockRequestTicketHistoryRepository { get; private set; }
        public Mock<IQueryRepository> MockQueryRepository { get; private set; }

        public RequestTicketService RequestTicketService { get; private set; }

        public RequestTicketServiceTestFixture()
        {
            MockRequestTicketRepository = new Mock<IRequestTicketRepository>();
            MockLogger = new Mock<ILogger<RequestTicketService>>();
            MockMapper = new Mock<IMapper>();
            MockUserRepository = new Mock<IUserRepository>();   
            MockServiceItemRepository = new Mock<IServiceItemRepository>();
            MockAttachmentService = new Mock<IAttachmentService>();
            MockSlaRepository = new Mock<ISlaRepository>();
            MockWorkflowAssignmentService = new Mock<IWorkflowAssignmentService>();
            MockRequestTicketHistoryService = new Mock<IRequestTicketHistoryService>();
            MockRequestTicketHistoryRepository = new Mock<IRequestTicketHistoryRepository>();
            MockQueryRepository = new Mock<IQueryRepository>();

            RequestTicketService = new RequestTicketService(
                MockRequestTicketRepository.Object,
                MockLogger.Object, MockMapper.Object,
                MockUserRepository.Object,
                MockServiceItemRepository.Object,
                MockAttachmentService.Object,
                MockSlaRepository.Object,
                MockWorkflowAssignmentService.Object,
                MockRequestTicketHistoryService.Object,
                MockRequestTicketHistoryRepository.Object,
                MockQueryRepository.Object);
            
        }

        [CollectionDefinition("RequestTicketServiceTests")]
        public class RequestTicketServiceCollection : ICollectionFixture<RequestTicketServiceTestFixture>
        {
        }
    }
}
