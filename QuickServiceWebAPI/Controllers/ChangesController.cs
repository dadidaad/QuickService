using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.Change;
using QuickServiceWebAPI.DTOs.Comment;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangesController : ControllerBase
    {
        private readonly IChangeService _changeService;
        public ChangesController(IChangeService changeService)
        {
            _changeService = changeService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllChange()
        {
            var changes = await _changeService.GetAllChanges();
            return Ok(changes);
        }

        [HttpGet("{changeId}")]
        public async Task<IActionResult> GetChange(string changeId)
        {
            var change = await _changeService.GetChange(changeId);
            return Ok(change);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateChange(CreateChangeDTO createChangeDTO)
        {
            var createdChange = await _changeService.CreateChange(createChangeDTO);
            return Ok(createdChange);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateComment([FromForm] UpdateChangeDTO updateChangeDTO)
        {
            await _changeService.UpdateChange(updateChangeDTO);
            return Ok(new { message = "Update successfully", errorCode = 0 });
        }
    }
}
