using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.DTOs.Comment;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Services.Implements;

namespace QuickServiceUnitTest
{
    public class CommentsControllerTest
    {
        private readonly CommentsController _controller;

        public CommentsControllerTest()
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<CommentRepository>>();
            var commentService = new CommentService(new CommentRepository(dbContext, mockLogger.Object),
                                                    new UserRepository(dbContext, new Mock<ILogger<UserRepository>>().Object),
                                                    new MapperConfiguration(cfg => cfg.CreateMap<Comment, CommentDTO>()).CreateMapper());

            _controller = new CommentsController(commentService);
        }

        [Fact]
        public void GetAllCommentByUser_ReturnsOkResult()
        {
            //Arrange
            string valid_userId = "USER000001";
            //string inValid_userId = "ABCDA123";

            //var errorResult = _controller.GetAllCommentByUser(inValid_userId);
            var successResult = _controller.GetAllCommentByUser(valid_userId);

            var resultType = successResult as OkObjectResult;
            var resultList = resultType.Value as List<CommentDTO>;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<OkObjectResult>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<List<CommentDTO>>(resultType.Value);
            Assert.Equal(16, resultList.Count);
        }

        [Fact]
        public void GetAllCustomerCommentByRequestTicket_ReturnsOkResult()
        {
            //Arrange
            string valid_requestTicketId = "RETK000001";
            //string inValid_requestTicketId = "ABCDA123";

            //var errorResult = _controller.GetAllCommentByRequestTicket(inValid_requestTicketId);
            var successResult = _controller.GetAllCommentByRequestTicket(valid_requestTicketId);

            var resultType = successResult as OkObjectResult;
            var resultList = resultType.Value as List<CommentDTO>;

            //assert
            //Assert.NotNull(errorResult);
            //Assert.IsType<OkObjectResult>(errorResult);

            Assert.NotNull(successResult);
            Assert.IsType<List<CommentDTO>>(resultType.Value);
            Assert.Equal(6, resultList.Count);
        }
    }
}
