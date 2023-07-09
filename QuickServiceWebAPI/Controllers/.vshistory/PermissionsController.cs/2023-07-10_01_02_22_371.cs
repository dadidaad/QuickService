using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Authentication;

namespace QuickServiceWebAPI.Controllers
{
    [HasPermission(PermissionEnum.ManageRoles, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }


        [HttpGet("get/{roleId}")]
        public async Task<IActionResult> GetPermissionByRole(string roleId)
        {
            return Ok(await _permissionService.GetPermissionsByRole(roleId));
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignPermissions(AssignPermissionsDTO assignPermissionsDTO)
        {
            await _permissionService.AssignPermissionsToRole(assignPermissionsDTO);
            return Ok(new { message = "Assign successfully" });
        }

        [HttpGet("get/{roleType:int}")]
        public async Task<IActionResult> GetPermissionByRoleType(RoleType roleType)
        {
            return Ok(await _permissionService.GetPermissionsByRoleType(roleType));
        }
    }
}
