﻿using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.Role;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [HasPermission(PermissionEnum.ManageRoles, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole(CreateDTO createDTO)
        {
            return Ok(new { message = "Create successfully", RoleDTO = await _roleService.CreateRole(createDTO) });
        }

        [HttpGet("getall")]
        public IActionResult GetAllRoles()
        {
            return Ok(_roleService.GetRoles());
        }

        [HttpGet("get/{roleId}")]
        public async Task<IActionResult> GetRole(string roleId)
        {
            var role = await _roleService.GetRoleById(roleId);
            return Ok(role);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateRole(UpdateDTO updateDTO)
        {
            await _roleService.UpdateRole(updateDTO);
            return Ok(new { message = "Update successfully" });
        }

        [HttpDelete("delete/{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            await _roleService.DeleteRole(roleId);
            return Ok(new { message = "Delete successfully" });
        }

        [HttpGet("get/{roleType:int}")]
        public IActionResult GetRoleByType(RoleType roleType)
        {
            return Ok(_roleService.GetRolesByType(roleType));
        }
    }
}
