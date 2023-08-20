//using AutoMapper;
//using Microsoft.Extensions.Logging;
//using Moq;
//using QuickServiceWebAPI.Controllers;
//using QuickServiceWebAPI.Models;
//using QuickServiceWebAPI.Repositories.Implements;
//using QuickServiceWebAPI.Repositories;
//using QuickServiceWebAPI.Services.Implements;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using QuickServiceWebAPI.DTOs.RequestTicket;
//using QuickServiceWebAPI.Services;
//using QuickServiceWebAPI.DTOs.Sla;
//using QuickServiceWebAPI.DTOs.ServiceItemCustomField;
//using QuickServiceWebAPI.DTOs.User;
//using QuickServiceWebAPI.DTOs.ServiceItem;
//using QuickServiceWebAPI.DTOs.Attachment;
//using QuickServiceWebAPI.DTOs.WorkflowAssignment;
//using QuickServiceWebAPI.DTOs.RequestTicketHistory;
//using QuickServiceWebAPI.DTOs.Group;
//using QuickServiceWebAPI.Utilities;
//using System.ComponentModel.DataAnnotations;
//using QuickServiceWebAPI.Models.Enums;

//namespace QuickServiceUnitTest
//{
//    public class RequestTicketsControllerTest
//    {
//        private readonly RequestTicketsController _controller;
//        public RequestTicketsControllerTest()
//        {
//            var dbContext = new QuickServiceContext();
//            var mockLogger = new Mock<ILogger<RequestTicketRepository>>();

//            var requestTicketService = new RequestTicketService(
//                new RequestTicketRepository(dbContext, mockLogger.Object),
//                new Mock<ILogger<RequestTicketService>>().Object,
//                new MapperConfiguration(cfg =>
//                {
//                    cfg.CreateMap<RequestTicket, RequestTicketDTO>();
//                    cfg.CreateMap<Sla, SlaDTO>();
//                    cfg.CreateMap<User, UserDTO>();
//                    cfg.CreateMap<ServiceItem, ServiceItemDTO>();
//                    cfg.CreateMap<Attachment, AttachmentDTO>();
//                    cfg.CreateMap<WorkflowAssignment, WorkflowAssignmentDTO>();
//                    cfg.CreateMap<RequestTicketHistory, RequestTicketHistoryDTO>();
//                    cfg.CreateMap<CreateRequestTicketDTO, RequestTicket>();
//                    cfg.CreateMap<UpdateRequestTicketDTO, RequestTicket>();
//                }).CreateMapper(),
//                new Mock<IUserRepository>().Object,
//                new Mock<IServiceItemRepository>().Object,
//                new Mock<IAttachmentService>().Object,
//                new Mock<ISlaRepository>().Object,
//                new Mock<IWorkflowAssignmentService>().Object,
//                new Mock<IRequestTicketHistoryService>().Object,
//                new Mock<IRequestTicketHistoryRepository>().Object
//                );
//            _controller = new RequestTicketsController(requestTicketService);
//        }

//        //[Fact]
//        //public async Task CreateRequestTicket_ValidInput_ReturnsOk()
//        //{
//        //    var createRequestTicketDTO = new CreateRequestTicketDTO
//        //    {
//        //        Title = "Title",
//        //        Description = "Description",
//        //        ServiceItemId = "SEIT000001",
//        //        RequesterEmail = "admin@quickservice.com"
//        //    };

//        //    var result = await _controller.SendRequestTicket(createRequestTicketDTO);

//        //    Assert.IsType<OkObjectResult>(result);
//        //}

//        [Fact]
//        public async Task CreateRequestTicket_MissingTitle_ReturnsOk()
//        {
//            var createRequestTicketDTO = new CreateRequestTicketDTO
//            {
//                Description = "Description",
//                ServiceItemId = "SEIT000001",
//                RequesterEmail = "admin@quickservice.com"
//            };

//            var context = new ValidationContext(createRequestTicketDTO, null, null);
//            var validationResults = new List<ValidationResult>();
//            var result = Validator.TryValidateObject(createRequestTicketDTO, context, validationResults, true);

//            // Assert
//            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Title field is required.");
//        }

//        [Fact]
//        public async Task CreateRequestTicket_InvalidTitle_ReturnsOk()
//        {
//            var createRequestTicketDTO = new CreateRequestTicketDTO
//            {
//                Title = new string('a', 501),
//                Description = "Description",
//                ServiceItemId = "SEIT000001",
//                RequesterEmail = "admin@quickservice.com"
//            };

//            var context = new ValidationContext(createRequestTicketDTO, null, null);
//            var validationResults = new List<ValidationResult>();
//            var result = Validator.TryValidateObject(createRequestTicketDTO, context, validationResults, true);

//            // Assert
//            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field Title must be a string or array type with a maximum length of '500'.");
//        }



//        [Fact]
//        public async Task CreateRequestTicket_InvalidDescription_ReturnsOk()
//        {
//            var createRequestTicketDTO = new CreateRequestTicketDTO
//            {
//                Title = "Title",
//                Description = new string('a', 1001),
//                ServiceItemId = "SEIT000001",
//                RequesterEmail = "admin@quickservice.com"
//            };

//            var context = new ValidationContext(createRequestTicketDTO, null, null);
//            var validationResults = new List<ValidationResult>();
//            var result = Validator.TryValidateObject(createRequestTicketDTO, context, validationResults, true);

//            // Assert
//            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field Description must be a string or array type with a maximum length of '1000'.");
//        }


//        [Fact]
//        public async Task CreateRequestTicket_InvalidServiceItemId_ReturnsOk()
//        {
//            var createRequestTicketDTO = new CreateRequestTicketDTO
//            {
//                Title = "Title",
//                Description = "Description",
//                ServiceItemId = new string('a', 11),
//                RequesterEmail = "admin@quickservice.com"
//            };

//            var context = new ValidationContext(createRequestTicketDTO, null, null);
//            var validationResults = new List<ValidationResult>();
//            var result = Validator.TryValidateObject(createRequestTicketDTO, context, validationResults, true);

//            // Assert
//            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field ServiceItemId must be a string or array type with a maximum length of '10'.");
//        }

//        [Fact]
//        public async Task CreateRequestTicket_MissingRequesterEmail_ReturnsOk()
//        {
//            var createRequestTicketDTO = new CreateRequestTicketDTO
//            {
//                Title = "Title",
//                Description = "Description",
//                ServiceItemId = "SEIT000001"
//            };

//            var context = new ValidationContext(createRequestTicketDTO, null, null);
//            var validationResults = new List<ValidationResult>();
//            var result = Validator.TryValidateObject(createRequestTicketDTO, context, validationResults, true);

//            // Assert
//            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The RequesterEmail field is required.");
//        }

//        [Fact]
//        public async Task CreateRequestTicket_InvalidRequesterEmail_ReturnsOk()
//        {
//            var createRequestTicketDTO = new CreateRequestTicketDTO
//            {
//                Title = "Title",
//                Description = "Description",
//                ServiceItemId = "SEIT000001",
//                RequesterEmail = "admin"
//            };

//            var context = new ValidationContext(createRequestTicketDTO, null, null);
//            var validationResults = new List<ValidationResult>();
//            var result = Validator.TryValidateObject(createRequestTicketDTO, context, validationResults, true);

//            // Assert
//            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The RequesterEmail field is not a valid e-mail address.");
//        }

//        [Fact]
//        public async Task CreateRequestTicket_InvalidRequesterEmailLength_ReturnsOk()
//        {
//            var createRequestTicketDTO = new CreateRequestTicketDTO
//            {
//                Title = "Title",
//                Description = "Description",
//                ServiceItemId = "SEIT000001",
//                RequesterEmail = new string('@', 101),
//            };

//            var context = new ValidationContext(createRequestTicketDTO, null, null);
//            var validationResults = new List<ValidationResult>();
//            var result = Validator.TryValidateObject(createRequestTicketDTO, context, validationResults, true);

//            // Assert
//            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field RequesterEmail must be a string or array type with a maximum length of '100'.");
//        }

//        //[Fact]
//        //public async Task UpdateRequestTicket_MissingTitle_ReturnsOk()
//        //{
//        //    var updateRequestTicketDTO = new UpdateRequestTicketDTO
//        //    {
//        //        RequestTicketId = "RETK000099",
//        //        Status = "Open",
//        //        Impact = "Low",
//        //        Urgency = "Low"
//        //    };

//        //    var result = await _controller.UpdateRequestTicket(updateRequestTicketDTO);

//        //    Assert.IsType<OkObjectResult>(result);
//        //}

//        [Fact]
//        public async Task UpdateeRequestTicket_MissingRequestTicketId_ReturnsOk()
//        {
//            var updateRequestTicketDTO = new UpdateRequestTicketDTO
//            {
//                Status = "Open",
//                Impact = "Low",
//                Urgency = "Low"
//            };

//            var context = new ValidationContext(updateRequestTicketDTO, null, null);
//            var validationResults = new List<ValidationResult>();
//            var result = Validator.TryValidateObject(updateRequestTicketDTO, context, validationResults, true);

//            // Assert
//            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The RequestTicketId field is required.");
//        }

//        [Fact]
//        public async Task UpdateeRequestTicket_InvalidTicketId_ReturnsOk()
//        {
//            var updateRequestTicketDTO = new UpdateRequestTicketDTO
//            {
//                RequestTicketId = new string('@', 11),
//                Status = "Open",
//                Impact = "Low",
//                Urgency = "Low"
//            };

//            var context = new ValidationContext(updateRequestTicketDTO, null, null);
//            var validationResults = new List<ValidationResult>();
//            var result = Validator.TryValidateObject(updateRequestTicketDTO, context, validationResults, true);

//            // Assert
//            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The field RequestTicketId must be a string or array type with a maximum length of '10'.");
//        }

//        [Fact]
//        public async Task UpdateeRequestTicket_MissingStatus_ReturnsOk()
//        {
//            var updateRequestTicketDTO = new UpdateRequestTicketDTO
//            {
//                RequestTicketId = "RETK000099",
//                Impact = "Low",
//                Urgency = "Low"
//            };

//            var context = new ValidationContext(updateRequestTicketDTO, null, null);
//            var validationResults = new List<ValidationResult>();
//            var result = Validator.TryValidateObject(updateRequestTicketDTO, context, validationResults, true);

//            // Assert
//            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Status field is required.");
//        }

//        [Fact]
//        public async Task UpdateeRequestTicket_MissingImpact_ReturnsOk()
//        {
//            var updateRequestTicketDTO = new UpdateRequestTicketDTO
//            {
//                RequestTicketId = "RETK000099",
//                Status = "Open",
//                Urgency = "Low"
//            };

//            var context = new ValidationContext(updateRequestTicketDTO, null, null);
//            var validationResults = new List<ValidationResult>();
//            var result = Validator.TryValidateObject(updateRequestTicketDTO, context, validationResults, true);

//            // Assert
//            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Impact field is required.");
//        }

//        [Fact]
//        public async Task UpdateeRequestTicket_MissingUrgency_ReturnsOk()
//        {
//            var updateRequestTicketDTO = new UpdateRequestTicketDTO
//            {
//                RequestTicketId = "RETK000099",
//                Status = "Open",
//                Impact = "Low"
//            };

//            var context = new ValidationContext(updateRequestTicketDTO, null, null);
//            var validationResults = new List<ValidationResult>();
//            var result = Validator.TryValidateObject(updateRequestTicketDTO, context, validationResults, true);

//            // Assert
//            Assert.Contains(validationResults, vr => vr.ErrorMessage == "The Urgency field is required.");
//        }

//        [Fact]
//        public async Task GetRequestTicket_ReturnsOkResult()
//        {
//            //Arrange
//            string valid_requestTicketId = "RETK000001";
//            //string inValid_requestTicketId = "ABCDA123";

//            //Act
//            //var errorResult = await _controller.GetRequestTicket(inValid_requestTicketId);
//            var successResult = await _controller.GetRequestTicket(valid_requestTicketId);

//            var successModel = successResult as OkObjectResult;
//            var fetchedGroup = successModel.Value as RequestTicketDTO;

//            //assert
//            //Assert.NotNull(errorResult);
//            //Assert.IsType<AppException>(errorResult);

//            Assert.NotNull(successResult);
//            Assert.IsType<OkObjectResult>(successResult);
//            Assert.IsType<RequestTicketDTO>(fetchedGroup);
//        }
//    }
//}
