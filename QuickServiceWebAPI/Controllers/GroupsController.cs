using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [HasPermission(PermissionEnum.ManageGroups, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllGroup()
        {
            var groups = _groupService.GetGroups();
            return Ok(groups);
        }

        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetWorkflowStepById(string groupId)
        {
            var group = await _groupService.GetGroupById(groupId);
            return Ok(group);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGroup(CreateUpdateGroupDTO createUpdateGroupDTO)
        {
            await _groupService.CreateGroup(createUpdateGroupDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateGroup(string groupId, CreateUpdateGroupDTO createUpdateGroupDTO)
        {
            await _groupService.UpdateGroup(groupId, createUpdateGroupDTO);
            return Ok(new { message = "Update successfully" });
        }

    }
}
