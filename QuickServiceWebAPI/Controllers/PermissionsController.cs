﻿using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

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


        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            return Ok(await _permissionService.GetPermissions());
        }

        [HttpGet("get/{roleId}")]
        public async Task<IActionResult> GetPermissionByRole(string roleId)
        {
            return Ok(await _permissionService.GetPermissionsForRoleWithDTO(roleId));
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignPermissions(AssignPermissionsDTO assignPermissionsDTO)
        {
            await _permissionService.AssignPermissionsToRole(assignPermissionsDTO);
            return Ok(new { message = "Assign successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePermissionForRole(UpdatePermissionsDTO updatePermissionsDTO)
        {
            await _permissionService.UpdatePermissionsToRole(updatePermissionsDTO);
            return Ok(new { message = "Update successfully" });
        }
    }
}
