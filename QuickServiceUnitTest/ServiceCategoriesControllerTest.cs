using AutoFixture;
using AutoMapper;
using Azure;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Win32;
using Moq;
using Newtonsoft.Json;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Utilities;
using Shouldly;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Xunit;

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
                cfg.CreateMap<CreateUpdateServiceCategoryDTO, ServiceCategory>();
            }).CreateMapper();

            var service = new ServiceCategoryService(repository, mapper);
            _controller = new ServiceCategoriesController(service);

        }

        //[Fact]
        //public async Task CreateServiceCategory_ValidInput_ReturnsOkWithServiceCategory()
        //{
        //    var createUpdateServiceCategoryDTO = new CreateUpdateServiceCategoryDTO
        //    {
        //        ServiceCategoryName = "ServiceCategoryName",
        //        Description = "Description"
        //    };

        //    var result = await _controller.CreateServiceCategory(createUpdateServiceCategoryDTO);

        //    Assert.IsType<OkObjectResult>(result);
        //}

        [Fact]
        public async Task CreateServiceCategory_MissingServiceCategoryName_ReturnsBadRequest()
        {
            // Arrange
            var createUpdateServiceCategoryDTO = new CreateUpdateServiceCategoryDTO
            {
                //No ServiceCategoryName
                Description = "Description"
            };

            var context = new ValidationContext(createUpdateServiceCategoryDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateServiceCategoryDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The ServiceCategoryName field is required.");
        }

        [Fact]
        public async Task CreateServiceCategory_InvalidServiceCategoryName_ReturnsBadRequest()
        {
            var createUpdateServiceCategoryDTO = new CreateUpdateServiceCategoryDTO
            {
                ServiceCategoryName = new string('a', 256), // Invalid ServiceCategoryName length
                Description = "ValidDescription"
            };

            var context = new ValidationContext(createUpdateServiceCategoryDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateServiceCategoryDTO, context, validationResults, true);

            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field ServiceCategoryName must be a string or array type with a maximum length of '100'.");
        }

        [Fact]
        public async Task CreateServiceCategory_InvalidDescription_ReturnsOkWithDefaultDescription()
        {
            var createUpdateServiceCategoryDTO = new CreateUpdateServiceCategoryDTO
            {
                ServiceCategoryName = "ABC",
                Description = new string('a', 256) // Invalid Description length
            };

            var context = new ValidationContext(createUpdateServiceCategoryDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateServiceCategoryDTO, context, validationResults, true);

            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field Description must be a string or array type with a maximum length of '255'.");
        }
    }
}