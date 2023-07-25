using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.BusinessHour;
using QuickServiceWebAPI.DTOs.Comment;
using QuickServiceWebAPI.DTOs.CustomField;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
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
            await _commentService.CreateComment(createCommentDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateComment(string commentId, UpdateCommentDTO updateCommentDTO)
        {
            await _commentService.UpdateComment(commentId, updateCommentDTO);
            return Ok(new { message = "Update successfully" });
        }
    }
}
