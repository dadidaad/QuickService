
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Authentication;
using QuickServiceWebAPI.Services.Implements;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;

namespace QuickServiceWebAPI.Controllers
{
    [Authorize]
    [HasPermission(PermissionEnum.ManageUsers, Models.RoleType.Admin)]
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(RegisterDTO registerDTO)
        {
            await _userService.CreateUser(registerDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpGet("getall")]
        public IActionResult GetAllUser()
        {
            //IEnumerable<User> users = _userService.GetUsers();
            //return Ok(users);
            var users = _userService.GetUsers();
            return Ok(users);
        }

        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser([FromForm]UpdateDTO updateDTO)
        {
            await _userService.UpdateUser(updateDTO);
            return Ok(new { message = "Update successfully" });
        }

        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignRole(AssignRoleDTO assignRoleDTO)
        {
            await _userService.AssignRole(assignRoleDTO);
            return Ok(new { message = "Assign successfully" });
        }

        [Authorize]
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            await _userService.ChangePassword(changePasswordDTO);
            return Ok(new { message = "Change password successfully" });
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            await _userService.ResetPassword(resetPasswordDTO);
            return Ok(new { message = "Reset password successfully" });
        }

    }
}
