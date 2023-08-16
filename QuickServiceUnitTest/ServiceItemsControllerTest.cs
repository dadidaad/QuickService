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
using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.DTOs.ServiceItemCustomField;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.DTOs.Group;

namespace QuickServiceUnitTest
{
    public class ServiceItemsControllerTest
    {
        private readonly ServiceItemsController _controller;
        public ServiceItemsControllerTest()
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<ServiceItemRepository>>();

            var serviceItemService = new ServiceItemService(
                new ServiceItemRepository(dbContext, mockLogger.Object),
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ServiceItemCustomField, ServiceItemCustomFieldDTO>();
                    cfg.CreateMap<ServiceCategory, ServiceCategoryDTO>();
                    cfg.CreateMap<ServiceItem, ServiceItemDTO>();
                    cfg.CreateMap<ServiceCategory, ServiceCategoryDTO>();
                    cfg.CreateMap<Workflow, WorkflowDTO>();
                }).CreateMapper(),
                Options.Create(new AzureStorageConfig()),
                new Mock<ILogger<ServiceItemService>>().Object,               
                new Mock<IServiceCategoryRepository>().Object,
                new Mock<IServiceItemCustomFieldRepository>().Object,
                new Mock<IWorkflowRepository>().Object
            );
            _controller = new ServiceItemsController(serviceItemService);
        }

        [Fact]
        public void GetAllServiceItem_ReturnsOkResult()
        {
            var result = _controller.GetAllServiceItem();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<ServiceItemDTO>;

            Assert.NotNull(result);
            Assert.IsType<List<ServiceItemDTO>>(resultType.Value);
            Assert.Equal(9, resultList.Count);
        }

        [Fact]
        public async Task GetServiceItemById_ReturnsOkResult()
        {
            //Arrange
            string valid_serviceItemId = "SEIT000001";
            //string inValid_serviceItemId = "ABCDA123";

            //Act
            //var errorResult = await _controller.GetServiceItemById(inValid_serviceItemId);
            var successResult = await _controller.GetServiceItemById(valid_serviceItemId);

            var successModel = successResult as OkObjectResult;
            var fetchedServiceItem = successModel.Value as ServiceItemDTO;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<OkObjectResult>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<ServiceItemDTO>(fetchedServiceItem);
        }
    }
}
