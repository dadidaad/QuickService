using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.Change;
using QuickServiceWebAPI.DTOs.CustomField;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.DTOs.ServiceItemCustomField;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                    cfg.CreateMap<CreateUpdateCustomFieldDTO, CustomField>();
                    cfg.CreateMap<ServiceItemCustomField, ServiceItemCustomFieldDTO>();
                }).CreateMapper(),             
                new Mock<IServiceItemCustomFieldRepository>().Object
                
            );
            _controller = new CustomFieldsController(customFieldService);
        }

        //[Fact]
        //public async Task CreateCustomField_ValidInput_ReturnsOk()
        //{
        //    var createUpdateCustomFieldDTO = new CreateUpdateCustomFieldDTO
        //    {
        //        FieldCode = "F0005",
        //        FieldName = "FieldName",
        //        FieldType = "FT",
        //        ValType = "T"
        //    };

        //    var result = await _controller.CreateCustomField(createUpdateCustomFieldDTO);

        //    Assert.IsType<OkObjectResult>(result);
        //}

        [Fact]
        public async Task CreateCustomField_MissingFieldCode_ReturnsBadRequest()
        {
            var createUpdateCustomFieldDTO = new CreateUpdateCustomFieldDTO
            {
                //Missing field code
                FieldName = "FieldName",
                FieldDescription = "Description",
                FieldType = "FT",
                ValType = "T",

            };

            var context = new ValidationContext(createUpdateCustomFieldDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateCustomFieldDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The FieldCode field is required.");
        }

        [Fact]
        public async Task CreateCustomField_InvalidFieldCode_ReturnsBadRequest()
        {
            var createUpdateCustomFieldDTO = new CreateUpdateCustomFieldDTO
            {
                FieldCode = new string('a', 51),
                FieldName = "FieldName",
                FieldDescription = "Description",               
                FieldType = "FT",
                ValType = "T",

            };

            var context = new ValidationContext(createUpdateCustomFieldDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateCustomFieldDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field FieldCode must be a string or array type with a maximum length of '50'.");
        }

        [Fact]
        public async Task CreateCustomField_MissingFieldName_ReturnsBadRequest()
        {
            var createUpdateCustomFieldDTO = new CreateUpdateCustomFieldDTO
            {
                FieldCode = "F0005",
                FieldDescription = "Description",
                FieldType = "FT",
                ValType = "T",

            };

            var context = new ValidationContext(createUpdateCustomFieldDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateCustomFieldDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The FieldName field is required.");
        }

        [Fact]
        public async Task CreateCustomField_InvalidFieldName_ReturnsBadRequest()
        {
            var createUpdateCustomFieldDTO = new CreateUpdateCustomFieldDTO
            {
                FieldCode = "F0005",
                FieldName = new string('a', 101),
                FieldDescription = "Description",
                FieldType = "FT",
                ValType = "T",

            };

            var context = new ValidationContext(createUpdateCustomFieldDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateCustomFieldDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field FieldName must be a string or array type with a maximum length of '100'.");
        }


        [Fact]
        public async Task CreateCustomField_InvalidFieldDescription_ReturnsBadRequest()
        {
            var createUpdateCustomFieldDTO = new CreateUpdateCustomFieldDTO
            {
                FieldCode = "F0005",
                FieldName = "FieldName",
                FieldDescription = new string('a', 301),
                FieldType = "FT",
                ValType = "T",

            };

            var context = new ValidationContext(createUpdateCustomFieldDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateCustomFieldDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field FieldDescription must be a string or array type with a maximum length of '300'.");
        }

        [Fact]
        public async Task CreateCustomField_MissingFieldType_ReturnsBadRequest()
        {
            var createUpdateCustomFieldDTO = new CreateUpdateCustomFieldDTO
            {
                FieldCode = "F0005",
                FieldName = "FieldName",
                FieldDescription = "Description",
                ValType = "T",

            };

            var context = new ValidationContext(createUpdateCustomFieldDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateCustomFieldDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The FieldType field is required.");
        }

        [Fact]
        public async Task CreateCustomField_InvalidFieldType_ReturnsBadRequest()
        {
            var createUpdateCustomFieldDTO = new CreateUpdateCustomFieldDTO
            {
                FieldCode = "F0005",
                FieldName = "FieldName",
                FieldDescription = "Description",
                FieldType = new string('a', 11),
                ValType = "T",

            };

            var context = new ValidationContext(createUpdateCustomFieldDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateCustomFieldDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field FieldType must be a string or array type with a maximum length of '10'.");
        }

        [Fact]
        public async Task CreateCustomField_MissingValType_ReturnsBadRequest()
        {
            var createUpdateCustomFieldDTO = new CreateUpdateCustomFieldDTO
            {
                FieldCode = "F0005",
                FieldName = "FieldName",
                FieldDescription = "Description",
                FieldType = "FT",

            };

            var context = new ValidationContext(createUpdateCustomFieldDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateCustomFieldDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The ValType field is required.");
        }

        [Fact]
        public async Task CreateCustomField_InvalidValType_ReturnsBadRequest()
        {
            var createUpdateCustomFieldDTO = new CreateUpdateCustomFieldDTO
            {
                FieldCode = "F0005",
                FieldName = "FieldName",
                FieldDescription = "Description",
                FieldType = "FT",
                ValType = new string('a', 11),

            };

            var context = new ValidationContext(createUpdateCustomFieldDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateCustomFieldDTO, context, validationResults, true);

            // Assert
            Assert.False(result);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field ValType must be a string or array type with a maximum length of '10'.");
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
