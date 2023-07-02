using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.ServiceType;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
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
            var serviceTypes = _groupService.GetGroups();
            return Ok(serviceTypes);
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
