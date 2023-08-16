using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.DTOs.BusinessHour;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.Service;
using QuickServiceWebAPI.DTOs.ServiceType;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest
{
    public class SlametricsControllerTest
    {
        private readonly SlametricsController _controller;

        public SlametricsControllerTest()
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<SlametricRepository>>();
            var slametricService = new SlametricService(
                new SlametricRepository(dbContext, mockLogger.Object),
                new Mock<IBusinessHourRepository>().Object,
                new Mock<ISlaRepository>().Object,
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Slametric, SlametricDTO>();
                    cfg.CreateMap<BusinessHour, BusinessHourDTO>();
                    cfg.CreateMap<Sla, SlaDTO>();
                }).CreateMapper()
            );

            _controller = new SlametricsController(slametricService);
        }

        [Fact]
        public void GetAllSLAmetric_ReturnsOkResult()
        {
            var result = _controller.GetAllSLAmetric();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<SlametricDTO>;

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<SlametricDTO>>(resultType.Value);
            Assert.Equal(13, resultList.Count);
        }

        [Fact]
        public async Task GetSLAById_ReturnsOkResult()
        {
            //Arrange
            string valid_slametricId = "SLAM000001";
            //string inValid_slametricId = "ABCDA123";

            //Act
            //var errorResult = await _controller.GetSLAById(inValid_slametricId);
            var successResult = await _controller.GetSLAById(valid_slametricId);

            var successModel = successResult as OkObjectResult;
            var fetchedSlametric = successModel.Value as SlametricDTO;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<AppException>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<SlametricDTO>(fetchedSlametric);
        }
    }
}
