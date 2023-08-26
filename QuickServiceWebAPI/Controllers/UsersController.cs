
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AuthenticateRequestDTO authenticateRequestDTO)
        {
            var response = await _userService.Authenticate(authenticateRequestDTO);
            return Ok(response);
        }

        [HasPermission(PermissionEnum.ManageUsers, RoleType.Admin)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(RegisterDTO registerDTO)
        {
            await _userService.CreateUser(registerDTO);
            return Ok(new { message = "Create successfully" });
        }

        [Authorize]
        [HttpGet("getall")]
        public IActionResult GetAllUser()
        {
            //IEnumerable<User> users = _userService.GetUsers();
            //return Ok(users);
            var users = _userService.GetUsers();
            return Ok(users);
        }

        [Authorize]
        [HttpPost("get/{userId}")]
        public async Task<IActionResult> GetById(string userId)
        {
            return Ok(await _userService.GetUserById(userId));
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDTO updateDTO)
        {
            await _userService.UpdateUser(updateDTO);
            return Ok(new { message = "Update successfully" });
        }

        [HasPermission(PermissionEnum.ManageUsers, RoleType.Admin)]
        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignRole(AssignRoleDTO assignRoleDTO)
        {
            await _userService.AssignRole(assignRoleDTO);
            return Ok(new { message = "Assign successfully" });
        }

        [HttpPost("changepassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            await _userService.ChangePassword(changePasswordDTO);
            return Ok(new { message = "Change password successfully" });
        }

        [HasPermission(PermissionEnum.ManageUsers, RoleType.Admin)]
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            await _userService.ResetPassword(resetPasswordDTO);
            return Ok(new { message = "Reset password successfully" });
        }

        [HasPermission(PermissionEnum.ManageUsers, RoleType.Admin)]
        [HttpPost("deactive")]
        public async Task<IActionResult> DeactiveUser(string userId)
        {
            await _userService.DeactiveUser(userId);
            return Ok(new { message = "Deactive user successfully" });
        }

        [Authorize]
        [HttpPost("search")]
        public async Task<IActionResult> SearchUser(ContainStringDTO containStringDTO)
        {
            return Ok(await _userService.GetUserByContainString(containStringDTO));
        }
    }
}
