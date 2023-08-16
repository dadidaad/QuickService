using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.DTOs.ServiceItemCustomField;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.WorkflowAssignment;
using QuickServiceWebAPI.DTOs.RequestTicketHistory;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceUnitTest
{
    public class RequestTicketsControllerTest
    {
        private readonly RequestTicketsController _controller;
        public RequestTicketsControllerTest()
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<RequestTicketRepository>>();

            var requestTicketService = new RequestTicketService(
                new RequestTicketRepository(dbContext, mockLogger.Object),
                new Mock<ILogger<RequestTicketService>>().Object,
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RequestTicket, RequestTicketDTO>();
                    cfg.CreateMap<Sla, SlaDTO>();
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<ServiceItem, ServiceItemDTO>();
                    cfg.CreateMap<Attachment, AttachmentDTO>();
                    cfg.CreateMap<WorkflowAssignment, WorkflowAssignmentDTO>();
                    cfg.CreateMap<RequestTicketHistory, RequestTicketHistoryDTO>();
                }).CreateMapper(),
                new Mock<IUserRepository>().Object,
                new Mock<IServiceItemRepository>().Object,
                new Mock<IAttachmentService>().Object,
                new Mock<ISlaRepository>().Object,
                new Mock<IWorkflowAssignmentService>().Object,
                new Mock<IRequestTicketHistoryService>().Object,
                new Mock<IRequestTicketHistoryRepository>().Object
                );
            _controller = new RequestTicketsController(requestTicketService);
        }

        [Fact]
        public async Task GetAllTickets_ReturnsOkResult()
        {
            var result = await _controller.GetAllTickets();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<RequestTicketDTO>;

            Assert.NotNull(result);
            Assert.IsType<List<RequestTicketDTO>>(resultType.Value);
            Assert.Equal(96, resultList.Count);
        }

        [Fact]
        public async Task GetRequestTicket_ReturnsOkResult()
        {
            //Arrange
            string valid_requestTicketId = "RETK000001";
            //string inValid_requestTicketId = "ABCDA123";

            //Act
            //var errorResult = await _controller.GetRequestTicket(inValid_requestTicketId);
            var successResult = await _controller.GetRequestTicket(valid_requestTicketId);

            var successModel = successResult as OkObjectResult;
            var fetchedGroup = successModel.Value as RequestTicketDTO;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<AppException>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<RequestTicketDTO>(fetchedGroup);
        }
    }
}
