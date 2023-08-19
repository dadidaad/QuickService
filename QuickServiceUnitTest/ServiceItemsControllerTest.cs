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
using System.ComponentModel.DataAnnotations;

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
                    cfg.CreateMap<CreateUpdateServiceItemDTO, ServiceItem>();
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

        //[Fact]
        //public async Task CreateServiceItem_ValidInput_ReturnsOk()
        //{
        //    var createUpdateServiceItemDTO = new CreateUpdateServiceItemDTO
        //    {
        //        ServiceItemName = "ServiceItemName",
        //        ShortDescription = "ShortDescription",
        //        Description = "Description",
        //        ServiceCategoryId = "SECA000002",
        //        IconDisplay = "IconDisplay",
        //    };

        //    var result = await _controller.CreateServiceItem(createUpdateServiceItemDTO);

        //    Assert.IsType<OkObjectResult>(result);
        //}

        [Fact]
        public async Task CreateServiceItem_MissingServiceItemName_ReturnsOk()
        {
            var createUpdateServiceItemDTO = new CreateUpdateServiceItemDTO
            {
                ShortDescription = "ShortDescription",
                Description = "Description",
                ServiceCategoryId = "SECA000002",
                IconDisplay = "IconDisplay",
            };

            var context = new ValidationContext(createUpdateServiceItemDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateServiceItemDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The ServiceItemName field is required.");
        }

        [Fact]
        public async Task CreateServiceItem_InvalidServiceItemName_ReturnsOk()
        {
            var createUpdateServiceItemDTO = new CreateUpdateServiceItemDTO
            {
                ServiceItemName = new string('a', 101),
                ShortDescription = "ShortDescription",
                Description = "Description",
                ServiceCategoryId = "SECA000002",
                IconDisplay = "IconDisplay",
            };

            var context = new ValidationContext(createUpdateServiceItemDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateServiceItemDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field ServiceItemName must be a string or array type with a maximum length of '100'.");
        }

        [Fact]
        public async Task CreateServiceItem_MissingShortDescription_ReturnsOk()
        {
            var createUpdateServiceItemDTO = new CreateUpdateServiceItemDTO
            {
                ServiceItemName = "ServiceItemName",
                Description = "Description",
                ServiceCategoryId = "SECA000002",
                IconDisplay = "IconDisplay",
            };

            var context = new ValidationContext(createUpdateServiceItemDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateServiceItemDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The ShortDescription field is required.");
        }

        [Fact]
        public async Task CreateServiceItem_InvalidShortDescription_ReturnsOk()
        {
            var createUpdateServiceItemDTO = new CreateUpdateServiceItemDTO
            {
                ServiceItemName = "ServiceItemName",
                ShortDescription = new string('a', 101),
                Description = "Description",
                ServiceCategoryId = "SECA000002",
                IconDisplay = "IconDisplay",
            };

            var context = new ValidationContext(createUpdateServiceItemDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateServiceItemDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field ShortDescription must be a string or array type with a maximum length of '100'.");
        }

        [Fact]
        public async Task CreateServiceItem_InvalidDescription_ReturnsOk()
        {
            var createUpdateServiceItemDTO = new CreateUpdateServiceItemDTO
            {
                ServiceItemName = "ServiceItemName",
                ShortDescription = "ShortDescription",
                Description = new string('a', 1001),
                ServiceCategoryId = "SECA000002",
                IconDisplay = "IconDisplay",
            };

            var context = new ValidationContext(createUpdateServiceItemDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateServiceItemDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field Description must be a string or array type with a maximum length of '1000'.");
        }

        [Fact]
        public async Task CreateServiceItem_MissingServiceCategoryId_ReturnsOk()
        {
            var createUpdateServiceItemDTO = new CreateUpdateServiceItemDTO
            {
                ServiceItemName = "ServiceItemName",
                ShortDescription = "ShortDescription",
                Description = "Description",
                IconDisplay = "IconDisplay",
            };

            var context = new ValidationContext(createUpdateServiceItemDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateServiceItemDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The ServiceCategoryId field is required.");
        }


        [Fact]
        public async Task CreateServiceItem_InvalidIconDisplay_ReturnsOk()
        {
            var createUpdateServiceItemDTO = new CreateUpdateServiceItemDTO
            {
                ServiceItemName = "ServiceItemName",
                ShortDescription = "ShortDescription",
                Description = "Description",
                ServiceCategoryId = "SECA000002",
                IconDisplay = new string('a', 101),
            };

            var context = new ValidationContext(createUpdateServiceItemDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateServiceItemDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field IconDisplay must be a string or array type with a maximum length of '100'.");
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
