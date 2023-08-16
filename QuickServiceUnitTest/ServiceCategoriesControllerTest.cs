using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;

namespace QuickServiceUnitTest
{
    public class ServiceCategoriesControllerTest
    {
        private readonly ServiceCategoriesController _controller;

        public ServiceCategoriesControllerTest()
        {
            var dbContext = new QuickServiceContext();

            var mockLogger = new Mock<ILogger<ServiceCategoryRepository>>();

            var repository = new ServiceCategoryRepository(dbContext, mockLogger.Object);
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ServiceCategory, ServiceCategoryDTO>();
                //cfg.CreateMap<CreateUpdateServiceCategoryDTO, ServiceCategory>();
            }).CreateMapper();

            var service = new ServiceCategoryService(repository, mapper);

            _controller = new ServiceCategoriesController(service);
        }

        [Fact]
        public void GetAllServiceCategory_ReturnsOkResult()
        {
            var result = _controller.GetAllServiceCategory();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<ServiceCategoryDTO>;

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<ServiceCategoryDTO>>(resultType.Value);
            Assert.Equal(6, resultList.Count);
        }

        [Fact]
        public async Task GetServiceCategoryById_ReturnsOkResult()
        {
            //Arrange
            string valid_serviceCategoryId = "SECA000001";
            //string inValid_serviceCategoryId = "ABCDA123";

            //Act
            //var errorResult = await _controller.GetServiceCategoryById(inValid_serviceCategoryId);
            var successResult = await _controller.GetServiceCategoryById(valid_serviceCategoryId);

            var successModel = successResult as OkObjectResult;
            var fetchedServiceCategory = successModel.Value as ServiceCategoryDTO;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<OkObjectResult>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<ServiceCategoryDTO>(fetchedServiceCategory);
        }
    }
}