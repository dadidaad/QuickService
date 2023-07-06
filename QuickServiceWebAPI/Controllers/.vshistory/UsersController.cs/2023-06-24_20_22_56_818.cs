
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticateRequestDTO authenticateRequestDTO)
        {
            var response = await _userService.Authenticate(authenticateRequestDTO);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(RegisterDTO registerDTO)
        {
            await _userService.CreateUser(registerDTO);
            return Ok(new { message = "Create successfully" });
        }

        [AllowAnonymous]
        [HttpGet("getall")]
        public IActionResult GetAllUser()
        {
            IEnumerable<User> users = _userService.GetUsers();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser([FromForm]UpdateDTO updateDTO)
        {
            await _userService.UpdateUser(updateDTO);
            return Ok(new { message = "Update successfully" });
        }
    }
}
