using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using QuickServiceWebAPI.Controllers;
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
using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.DTOs.WorkflowTask;
using QuickServiceWebAPI.DTOs.WorkflowAssignment;
using QuickServiceWebAPI.DTOs.RequestTicket;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace QuickServiceUnitTest
{
    public class WorkflowsControllerTest
    {
        private readonly WorkflowsController _controller;

        public WorkflowsControllerTest()
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<WorkflowRepository>>();
            var workflowService = new WorkflowService(
                new WorkflowRepository(dbContext, mockLogger.Object),
                new Mock<IUserRepository>().Object,
                 new MapperConfiguration(cfg =>
                 {
                     cfg.CreateMap<Workflow, WorkflowDTO>();
                     cfg.CreateMap<CreateUpdateWorkflowDTO, Workflow > ();
                     cfg.CreateMap<ServiceItem, ServiceItemDTO>();
                     cfg.CreateMap<Sla, SlaDTO>();
                     cfg.CreateMap<WorkflowTask, WorkflowTaskDTO>();
                     cfg.CreateMap<WorkflowAssignment, WorkflowAssignmentDTO>();
                     cfg.CreateMap<RequestTicket, RequestTicketDTO>();
                 }).CreateMapper(),
                new Mock<IServiceItemRepository>().Object,
                new Mock<ISlaRepository>().Object,
                new Mock<IWorkflowTaskService>().Object,
                new Mock<IWorkflowAssignmentRepository>().Object,
                new Mock<IRequestTicketRepository>().Object
            );

            _controller = new WorkflowsController(workflowService);
        }

        //[Fact]
        //public async Task CreateWorkflow_ValidInput_ReturnsOk()
        //{
        //    var createUpdateWorkflowDTO = new CreateUpdateWorkflowDTO
        //    {
        //        WorkflowName = "WorkflowName",
        //        Description = "Description",
        //        CreatedBy = "USER000001"
        //    };

        //    var result = await _controller.CreateWorkflow(createUpdateWorkflowDTO);

        //    Assert.IsType<OkObjectResult>(result);
        //}

        [Fact]
        public async Task CreateWorkflow_MissingWorkflowName_ReturnsOk()
        {
            var createUpdateWorkflowDTO = new CreateUpdateWorkflowDTO
            {
                Description = "Description",
                CreatedBy = "USER000001"
            };

            var context = new ValidationContext(createUpdateWorkflowDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateWorkflowDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The WorkflowName field is required.");
        }


        [Fact]
        public async Task CreateWorkflow_InvalidWorkflowName_ReturnsOk()
        {
            var createUpdateWorkflowDTO = new CreateUpdateWorkflowDTO
            {
                WorkflowName = new string('a', 256),
                Description = "Description",
                CreatedBy = "USER000001"
            };

            var context = new ValidationContext(createUpdateWorkflowDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateWorkflowDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field WorkflowName must be a string or array type with a maximum length of '255'.");
        }

        [Fact]
        public async Task CreateWorkflow_MissingDescription_ReturnsOk()
        {
            var createUpdateWorkflowDTO = new CreateUpdateWorkflowDTO
            {
                WorkflowName = "WorkflowName",
                CreatedBy = "USER000001"
            };

            var context = new ValidationContext(createUpdateWorkflowDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateWorkflowDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Description field is required.");
        }

        [Fact]
        public async Task CreateWorkflow_InvalidDescription_ReturnsOk()
        {
            var createUpdateWorkflowDTO = new CreateUpdateWorkflowDTO
            {
                WorkflowName = "WorkflowName",
                Description = new string('a', 1001),
                CreatedBy = "USER000001"
            };

            var context = new ValidationContext(createUpdateWorkflowDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateWorkflowDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field Description must be a string or array type with a maximum length of '1000'.");
        }

        [Fact]
        public async Task CreateWorkflow_MissingCreatedBy_ReturnsOk()
        {
            var createUpdateWorkflowDTO = new CreateUpdateWorkflowDTO
            {
                WorkflowName = "WorkflowName",
                Description = "Description"
            };

            var context = new ValidationContext(createUpdateWorkflowDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateWorkflowDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The CreatedBy field is required.");
        }

        [Fact]
        public async Task CreateWorkflow_InvalidCreatedBy_ReturnsOk()
        {
            var createUpdateWorkflowDTO = new CreateUpdateWorkflowDTO
            {
                WorkflowName = "WorkflowName",
                Description = "Description",
                CreatedBy = new string('a', 11)
            };

            var context = new ValidationContext(createUpdateWorkflowDTO, null, null);
            var validationResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(createUpdateWorkflowDTO, context, validationResults, true);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field CreatedBy must be a string or array type with a maximum length of '10'.");
        }
    }
}
