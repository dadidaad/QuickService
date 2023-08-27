using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.Comment;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetAllCommentByUser(string userId)
        {
            var comments = _commentService.GetCommentByUser(userId);
            return Ok(comments);
        }

        [HttpGet("customer/{requestTicketId}")]
        public IActionResult GetAllCustomerCommentByRequestTicket(string requestTicketId)
        {
            var comments = _commentService.GetCustomerCommentsByRequestTicket(requestTicketId);
            return Ok(comments);
        }

        [HttpGet("getall/{requestTicketId}")]
        public IActionResult GetAllCommentByRequestTicket(string requestTicketId)
        {
            var comments = _commentService.GetCommentsByRequestTicket(requestTicketId);
            return Ok(comments);
        }

        [HttpGet("customer/change/{changeId}")]
        public IActionResult GetAllCustomerCommentByChange(string changeId)
        {
            var comments = _commentService.GetCustomerCommentsByChange(changeId);
            return Ok(comments);
        }

        [HttpGet("getall/change/{changeId}")]
        public IActionResult GetAllCommentByChange(string changeId)
        {
            var comments = _commentService.GetCommentsByChange(changeId);
            return Ok(comments);
        }

        [HttpGet("customer/problem/{problemId}")]
        public IActionResult GetAllCustomerCommentByProblem(string problemId)
        {
            var comments = _commentService.GetCustomerCommentsByProblem(problemId);
            return Ok(comments);
        }

        [HttpGet("getall/problem/{problemId}")]
        public IActionResult GetAllCommentByProblem(string problemId)
        {
            var comments = _commentService.GetCommentsByProblem(problemId);
            return Ok(comments);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateComment(CreateCommentDTO createCommentDTO)
        {
            var createdComment = await _commentService.CreateComment(createCommentDTO);
            return Ok(createdComment);
        }

        [HttpPost("createCommentChange")]
        public async Task<IActionResult> CreateCommentChange(CreateCommentChangeDTO createCommentChangeDTO)
        {
            var createdComment = await _commentService.CreateCommentChange(createCommentChangeDTO);
            return Ok(createdComment);
        }

        [HttpPost("createCommentProblem")]
        public async Task<IActionResult> CreateCommentProblem(CreateCommentProblemDTO createCommentProblemDTO)
        {
            var createdComment = await _commentService.CreateCommentProblem(createCommentProblemDTO);
            return Ok(createdComment);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateComment(UpdateCommentDTO updateCommentDTO)
        {
            await _commentService.UpdateComment(updateCommentDTO);
            return Ok(new { message = "Update successfully", errorCode = 0 });
        }

        [HttpDelete("delete/{commentId}")]
        public async Task<IActionResult> DeleteComment(string commentId)
        {
            await _commentService.DeleteComment(commentId);
            return Ok(new { message = "Delete successfully", errorCode = 0 });
        }
    }
}
