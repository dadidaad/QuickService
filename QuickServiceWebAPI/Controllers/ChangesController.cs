using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.Services;

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
    }
}
