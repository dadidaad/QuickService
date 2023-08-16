using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.Service;
using QuickServiceWebAPI.DTOs.ServiceType;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest
{
    public class SlasControllerTest
    {
        private readonly SlasController _controller;

        public SlasControllerTest()
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<SlaRepository>>();
            var slaService = new SlaService(
                new SlaRepository(dbContext, mockLogger.Object),
                 new MapperConfiguration(cfg =>
                 {
                     cfg.CreateMap<Sla, SlaDTO>();
                     cfg.CreateMap<Slametric, SlametricDTO>();
                 }).CreateMapper(),
                new Mock<ISlametricService>().Object,
                new Mock<ISlametricRepository>().Object                         
            );

            _controller = new SlasController(slaService);
        }

        [Fact]
        public void GetAllSLA_ReturnsOkResult()
        {
            var result = _controller.GetAllSLA();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<SlaDTO>;

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<SlaDTO>>(resultType.Value);
            Assert.Equal(4, resultList.Count);
        }

        [Fact]
        public async Task GetSLAById_ReturnsOkResult()
        {
            //Arrange
            string valid_slaId = "SELA000001";
            //string inValid_slaId = "ABCDA123";

            //Act
            //var errorResult = await _controller.GetSLAById(inValid_slaId);
            var successResult = await _controller.GetSLAById(valid_slaId);

            var successModel = successResult as OkObjectResult;
            var fetchedSla = successModel.Value as SlaDTO;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<AppException>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<SlaDTO>(fetchedSla);
        }
    }
}
