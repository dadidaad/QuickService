using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.CustomField;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.Service;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.DTOs.ServiceItemCustomField;
using QuickServiceWebAPI.DTOs.ServiceType;
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
    public class ServicesControllerTest
    {
        private readonly ServicesController _controller;

        public ServicesControllerTest()
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<ServiceRepository>>();
            var serviceService = new ServiceService(
                new ServiceRepository(dbContext, mockLogger.Object),
                new Mock<IUserRepository>().Object,
                new Mock<IGroupRepository>().Object,
                new Mock<IServiceTypeRepository>().Object,
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Service, ServiceDTO>();
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<Group, GroupDTO>();
                    cfg.CreateMap<ServiceType, ServiceTypeDTO>();
                }).CreateMapper()
            );          
            _controller = new ServicesController(serviceService);
        }

        [Fact]
        public void GetAllService_ReturnsOkResult()
        {
            var result = _controller.GetAllService();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<ServiceDTO>;

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<ServiceDTO>>(resultType.Value);
            Assert.Equal(1, resultList.Count);
        }

        [Fact]
        public async Task GetServiceById_ReturnsOkResult()
        {
            //Arrange
            string valid_serviceById = "SERV000001";
            //string inValid_serviceById = "ABCDA123";

            //Act
            //var errorResult = await _controller.GetServiceById(inValid_serviceById);
            var successResult = await _controller.GetServiceById(valid_serviceById);

            var successModel = successResult as OkObjectResult;
            var fetchedService = successModel.Value as ServiceDTO;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<AppException>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<ServiceDTO>(fetchedService);
        }
    }
}
