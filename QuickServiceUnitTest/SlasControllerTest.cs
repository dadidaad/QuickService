using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.DTOs.Group;
using System.ComponentModel.DataAnnotations;

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
                     cfg.CreateMap<CreateSlaDTO, Sla>();
                 }).CreateMapper(),
                new Mock<ISlametricService>().Object,
                new Mock<ISlametricRepository>().Object
            );

            _controller = new SlasController(slaService);
        }

        //[Fact]
        //public async Task CreateSla_ValidInput_ReturnsOk()
        //{
        //    var createSlaDTO = new CreateSlaDTO
        //    {
        //        Slaname = "Slaname",
        //        Description = "Description"
        //    };

        //    var result = await _controller.CreateSLA(createSlaDTO);

        //    Assert.IsType<OkObjectResult>(result);
        //}

        [Fact]
        public async Task CreateSla_MissingSlaname_ReturnsOk()
        {
            var createSlaDTO = new CreateSlaDTO
            {
                Description = "Description"
            };

            var context = new ValidationContext(createSlaDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createSlaDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Slaname field is required.");
        }

        [Fact]
        public async Task CreateSla_InvalidSlaname_ReturnsOk()
        {
            var createSlaDTO = new CreateSlaDTO
            {
                Slaname = new string('a', 101),
                Description = "Description"
            };

            var context = new ValidationContext(createSlaDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createSlaDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field Slaname must be a string or array type with a maximum length of '100'.");
        }

        [Fact]
        public async Task CreateSla_InvalidDescription_ReturnsOk()
        {
            var createSlaDTO = new CreateSlaDTO
            {
                Slaname = "Slaname",
                Description = new string('a', 256)
            };

            var context = new ValidationContext(createSlaDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createSlaDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field Description must be a string or array type with a maximum length of '255'.");
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
