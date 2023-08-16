using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.Comment;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest
{
    public class AttachmentsControllerTest
    {
        private readonly AttachmentsController _controller;

        public AttachmentsControllerTest()
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<AttachmentRepository>>();
            var attachmentService = new AttachmentService(
                new AttachmentRepository(dbContext, mockLogger.Object),
                new MapperConfiguration(cfg => cfg.CreateMap<Attachment, AttachmentDTO>()).CreateMapper(),
                Options.Create(new AzureStorageConfig()),
                new Mock<ILogger<AttachmentService>>().Object
            );

            _controller = new AttachmentsController(attachmentService);
        }

        [Fact]
        public void GetAllAttachment_ReturnsOkResult()
        {
            var result = _controller.GetAllAttachment();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<AttachmentDTO>;

            Assert.NotNull(result);
            Assert.IsType<List<AttachmentDTO>>(resultType.Value);
            Assert.Equal(28, resultList.Count);
        }

        [Fact]
        public async Task GetAttachmentById_ReturnsOkResult()
        {
            //Arrange
            string valid_serviceCategoryId = "ATTA000001";
            //string inValid_serviceCategoryId = "ABCDA123";

            //Act
            //var errorResult = await _controller.GetAttachmentById(inValid_serviceCategoryId);
            var successResult = await _controller.GetAttachmentById(valid_serviceCategoryId);

            var successModel = successResult as OkObjectResult;
            var fetchedAttachment = successModel.Value as AttachmentDTO;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<OkObjectResult>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<OkObjectResult>(successResult);
            Assert.IsType<AttachmentDTO>(fetchedAttachment);
        }
    }
}
