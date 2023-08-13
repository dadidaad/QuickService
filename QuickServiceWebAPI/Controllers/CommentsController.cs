using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.Comment;
using QuickServiceWebAPI.Models;
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateComment(CreateCommentDTO createCommentDTO)
        {
            var createdComment = await _commentService.CreateComment(createCommentDTO);
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
