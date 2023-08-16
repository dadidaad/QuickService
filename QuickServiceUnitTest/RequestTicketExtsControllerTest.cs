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
using QuickServiceWebAPI.DTOs.CustomField;
using QuickServiceWebAPI.DTOs.ServiceItemCustomField;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.RequestTicketExt;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.RequestTicket;

namespace QuickServiceUnitTest
{
    public class RequestTicketExtsControllerTest
    {
        private readonly RequestTicketExtsController _controller;
        public RequestTicketExtsControllerTest()
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<RequestTicketExtRepository>>();

            var requestTicketExtService = new RequestTicketExtService(
                new RequestTicketExtRepository(dbContext, mockLogger.Object),
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RequestTicketExt, RequestTicketExtDTO>();
                    cfg.CreateMap<CustomField, CustomFieldDTO>();
                    cfg.CreateMap<RequestTicket, RequestTicketDTO>();
                }).CreateMapper(),
                new Mock<ICustomFieldRepository>().Object,               
                new Mock<IRequestTicketRepository>().Object

            );
            _controller = new RequestTicketExtsController(requestTicketExtService);
        }

        [Fact]
        public async Task GetAllRequestTicketExt_ReturnsOkResult()
        {
            var result = _controller.GetAllRequestTicketExt();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<RequestTicketExtDTO>;

            Assert.NotNull(result);
            Assert.IsType<List<RequestTicketExtDTO>>(resultType.Value);
            Assert.Equal(40, resultList.Count);
        }

        [Fact]
        public async Task GetRequestTicketExtById_ReturnsOkResult()
        {
            //Arrange
            string valid_groupId = "RETE000001";
            //string inValid_groupId = "ABCDA123";

            //Act
            //var errorResult = await _controller.GetRequestTicketExtById(inValid_groupId);
            var successResult = await _controller.GetRequestTicketExtById(valid_groupId);

            var successModel = successResult as OkObjectResult;
            var fetchedRequestTicketExt = successModel.Value as RequestTicketExtDTO;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<OkObjectResult>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<RequestTicketExtDTO>(fetchedRequestTicketExt);
        }
    }
}
