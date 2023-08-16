using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.Change;
using QuickServiceWebAPI.DTOs.CustomField;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.ServiceItemCustomField;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest
{
    public class CustomFieldsControllerTest
    {
        private readonly CustomFieldsController _controller;
        public CustomFieldsControllerTest() 
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<CustomFieldRepository>>();

            var customFieldService = new CustomFieldService(
                new CustomFieldRepository(dbContext, mockLogger.Object),
                new Mock<ILogger<CustomFieldService>>().Object,
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CustomField, CustomFieldDTO>();
                    cfg.CreateMap<ServiceItemCustomField, ServiceItemCustomFieldDTO>();
                }).CreateMapper(),             
                new Mock<IServiceItemCustomFieldRepository>().Object
                
            );
            _controller = new CustomFieldsController(customFieldService);
        }

        [Fact]
        public async Task GetAllCustomField_ReturnsOkResult()
        {
            var result = _controller.GetAllCustomField();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<CustomFieldDTO>;

            Assert.NotNull(result);
            Assert.IsType<List<CustomFieldDTO>>(resultType.Value);
            Assert.Equal(4, resultList.Count);
        }

        [Fact]
        public async Task GetCustomField_ReturnsOkResult()
        {
            //Arrange
            string valid_customFieldId = "CUFD000001";
            //string inValid_customFieldId = "ABCDA123";

            //Act
            //var errorResult = await _controller.GetCustomField(inValid_customFieldId);
            var successResult = await _controller.GetCustomField(valid_customFieldId);

            var successModel = successResult as OkObjectResult;
            var fetchedCustomField = successModel.Value as CustomFieldDTO;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<OkObjectResult>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<CustomFieldDTO>(fetchedCustomField);
        }
    }
}
