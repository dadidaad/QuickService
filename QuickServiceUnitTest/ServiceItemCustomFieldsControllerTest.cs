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
using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.DTOs.Role;
using QuickServiceWebAPI.DTOs.ServiceItemCustomField;
using QuickServiceWebAPI.DTOs.CustomField;
using QuickServiceWebAPI.DTOs.ServiceItem;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceUnitTest
{
    public class ServiceItemCustomFieldsControllerTest
    {
        private readonly ServiceItemCustomFieldsController _controller;
        public ServiceItemCustomFieldsControllerTest()
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<ServiceItemCustomFieldRepository>>();

            var roleService = new ServiceItemCustomFieldService(
                new ServiceItemCustomFieldRepository(dbContext, mockLogger.Object),
                new Mock<ILogger<ServiceItemCustomFieldService>>().Object,
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ServiceItemCustomField, ServiceItemCustomFieldDTO>();
                    cfg.CreateMap<ServiceItem, ServiceItemDTO>();
                    cfg.CreateMap<CustomField, CustomFieldDTO>();
                    cfg.CreateMap<Permission, PermissionDTO>();
                }).CreateMapper(),
            new Mock<IServiceItemRepository>().Object,
            new Mock<ICustomFieldRepository>().Object,
            new Mock<IServiceItemCustomFieldRepository>().Object
            );
            _controller = new ServiceItemCustomFieldsController(roleService);
        }

        [Fact]
        public async Task GetServiceItemCustomFieldByServiceItem_ReturnsOkResult()
        {
            //Arrange
            string valid_serviceItemId = "SEIT000001";
            //string inValid_serviceItemId = "SEIT000001";

            //Act
            //var errorResult = await _controller.GetServiceItemCustomFieldByServiceItem(inValid_serviceItemId);
            var successResult = await _controller.GetServiceItemCustomFieldByServiceItem(valid_serviceItemId);

            var successModel = successResult as OkObjectResult;
            var fetchedServiceItemCustomField = successModel.Value as ServiceItemCustomFieldDTO;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<AppException>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<ServiceItemCustomFieldDTO>(fetchedServiceItemCustomField);
        }
    }
}
