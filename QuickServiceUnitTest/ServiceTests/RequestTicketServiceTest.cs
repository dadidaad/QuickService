using Moq;
using QuickServiceUnitTest.ServiceTests.DerivedClasses;
using QuickServiceUnitTest.ServiceTests.Fixtures;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.RequestTicketHistory;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests
{
    [CollectionDefinition("RequestTicketServiceTests")]
    public class RequestTicketServiceTest
    {
        private readonly RequestTicketServiceTestFixture _fixture;

        public RequestTicketServiceTest()
        {
            _fixture = new RequestTicketServiceTestFixture();
        }

        [Fact]
        public async Task SendRequestTicket_ValidEmail_Success()
        {
            // Arrange
            var requestTicketService = _fixture.RequestTicketService;

            var requester = new User
            {
                Email = "newuser@example.com",
            };

            var serviceItem = new ServiceItem
            {
                ServiceItemId = "SEIT1",
            };

            var createRequestTicketDTO = new CreateRequestTicketDTO()
            {
                Title = "Test",
                Description = "Test",
                RequesterEmail = requester.Email
            };

            _fixture.MockUserRepository.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((User?)requester);
            _fixture.MockServiceItemRepository.Setup(repo => repo.GetServiceItemById(It.IsAny<string>())).ReturnsAsync((ServiceItem?)serviceItem);
            _fixture.MockMapper.Setup(mapper => mapper.Map<RequestTicket>(It.IsAny<CreateRequestTicketDTO>())).Returns(new RequestTicket());
            _fixture.MockMapper.Setup(mapper => mapper.Map<RequestTicket>(createRequestTicketDTO)).Returns(new RequestTicket { });
            // Act
            await requestTicketService.SendRequestTicket(createRequestTicketDTO);

            // Assert
            _fixture.MockRequestTicketRepository.Verify(repo => repo.AddRequestTicket(It.IsAny<RequestTicket>()), Times.Once);
        }

        [Fact]
        public async Task SendRequestTicket_InvalidEmail_ThrowsException()
        {

            var requestTicketService = _fixture.RequestTicketService;

            var requester = new User
            {
                Email = "notfounduser@example.com",
            };

            var serviceItem = new ServiceItem
            {
                ServiceItemId = "SEIT1",
            };

            var createRequestTicketDTO = new CreateRequestTicketDTO()
            {
                Title = "Test",
                Description = "Test",
                RequesterEmail = requester.Email
            };

            _fixture.MockUserRepository.Setup(repo => repo.GetUserByEmail(requester.Email)).ReturnsAsync((User?)null);

            // Act & Assert
            await Assert.ThrowsAsync<AppException>(async () => await requestTicketService.SendRequestTicket(createRequestTicketDTO));
        }

        [Fact]
        public async Task SendRequestTicket_InvalidServiceItem_ThrowsException()
        {

            var requestTicketService = _fixture.RequestTicketService;

            var requester = new User
            {
                Email = "exituser@example.com",
            };

            var serviceItem = new ServiceItem
            {
                ServiceItemId = "SEIT1",
            };

            var createRequestTicketDTO = new CreateRequestTicketDTO()
            {
                Title = "Test",
                Description = "Test",
                RequesterEmail = requester.Email
            };

            _fixture.MockServiceItemRepository.Setup(repo => repo.GetServiceItemById(serviceItem.ServiceItemId)).ReturnsAsync((ServiceItem?)null);
            // Act & Assert
            await Assert.ThrowsAsync<AppException>(async () => await requestTicketService.SendRequestTicket(createRequestTicketDTO));
        }

        [Fact]
        public async Task UpdateRequestTicket_ValidData_UpdatesRequestTicketSuccessfully()
        {

            var testableRequestTicketService = new TestableRequestTicketService(
                _fixture.MockRequestTicketRepository.Object,
                _fixture.MockLogger.Object,
                _fixture.MockMapper.Object,
                _fixture.MockUserRepository.Object,
                _fixture.MockServiceItemRepository.Object,
                _fixture.MockAttachmentService.Object,
                _fixture.MockSlaRepository.Object,
                _fixture.MockWorkflowAssignmentService.Object,
                _fixture.MockRequestTicketHistoryService.Object,
                _fixture.MockRequestTicketHistoryRepository.Object,
                _fixture.MockQueryRepository.Object
            );

            var existingRequestTicket = new RequestTicket
            {
                RequestTicketId = "RETK1"
            };

            var updateDTO = new UpdateRequestTicketDTO
            {
                RequestTicketId = existingRequestTicket.RequestTicketId,
                Status = "Open",
                Impact = "Low",
                Urgency = "Low"
            };

            

            _fixture.MockRequestTicketRepository.Setup(repo => repo.GetRequestTicketById(It.IsAny<string>()))
                .ReturnsAsync(existingRequestTicket);

            _fixture.MockMapper.Setup(map => map.Map(It.IsAny<UpdateRequestTicketDTO>(), It.IsAny<RequestTicket>()))
                .Returns(existingRequestTicket);
            // Act
            await testableRequestTicketService.UpdateRequestTicket(updateDTO);

            // Assert
            _fixture.MockRequestTicketRepository.Verify(repo => repo.UpdateRequestTicket(existingRequestTicket), Times.Once);
        }

        [Fact]
        public async Task UpdateRequestTicket_RequestTicketNotFound_ThrowsAppException()
        {

            var requestTicketService = _fixture.RequestTicketService;

            var updateDTO = new UpdateRequestTicketDTO
            {
                RequestTicketId = "RETK1",
                // Other properties
            };

            _fixture.MockRequestTicketRepository.Setup(repo => repo.GetRequestTicketById(It.IsAny<string>()))
                .ReturnsAsync((RequestTicket?)null); // Simulate user not found

            // Act and Assert
            await Assert.ThrowsAsync<AppException>(async () => await requestTicketService.UpdateRequestTicket(updateDTO));
        }


    }
}
