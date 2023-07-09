using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.Role;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> CreateRole(CreateDTO createDTO)
        {
            await _roleService.CreateRole(createDTO);
            return Ok(new { message = "Create successfully" });
        }

        [AllowAnonymous]
        [HttpGet("getall")]
        public IActionResult GetAllRoles()
        {
            return Ok(_roleService.GetRoles());
        }

        [AllowAnonymous]
        [HttpGet("get/{roleId}")]
        public async Task<IActionResult> GetRole(string roleId)
        {
            var role = await _roleService.GetRoleById(roleId);
            return Ok(role);
        }

        [AllowAnonymous]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateRole(UpdateDTO updateDTO)
        {
            await _roleService.UpdateRole(updateDTO);
            return Ok(new { message = "Update successfully" });
        }

        [AllowAnonymous]
        [HttpDelete("delete/{roleId}")] 
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            await _roleService.DeleteRole(roleId);
            return Ok(new { message = "Delete successfully" });
        }

        [AllowAnonymous]
        [HttpGet("get/{roleType:int}")]
        public IActionResult GetRoleByType(RoleType roleType)
        {
            return Ok(_roleService.GetRolesByType(roleType));
        }
    }
}
