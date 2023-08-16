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
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.DTOs.RequestTicketHistory;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.DTOs.Comment;

namespace QuickServiceUnitTest
{
    public class RequestTicketHistoriesControllerTest
    {
        private readonly RequestTicketHistoriesController _controller;
        public RequestTicketHistoriesControllerTest()
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<RequestTicketHistoryRepository>>();

            var requestTicketHistoryService = new RequestTicketHistoryService(
                new RequestTicketHistoryRepository(dbContext, mockLogger.Object),
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RequestTicketHistory, RequestTicketHistoryDTO>();
                }).CreateMapper()

            );
            _controller = new RequestTicketHistoriesController(requestTicketHistoryService);
        }

        [Fact]
        public async Task GetRequestTicketHistoryByRequestTicketId_ReturnsOkResult()
        {
            //Arrange
            string valid_requestTicketId = "RETK000070";
            //string inValid_requestTicketId = "ABCDA123";

            //var errorResult = await _controller.GetRequestTicketHistoryByRequestTicketId(inValid_requestTicketId);
            var successResult = await _controller.GetRequestTicketHistoryByRequestTicketId(valid_requestTicketId);

            var resultType = successResult as OkObjectResult;
            var resultList = resultType.Value as List<RequestTicketHistoryDTO>;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<OkObjectResult>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<List<RequestTicketHistoryDTO>>(resultType.Value);
            Assert.Equal(5, resultList.Count);
        }
    }
}
