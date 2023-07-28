using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [HasPermission(PermissionEnum.ManageSlas, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class SlasController : ControllerBase
    {
        private readonly ISlaService _slaService;
        public SlasController(ISlaService slaService)
        {
            _slaService = slaService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllSLA()
        {
            var slas = _slaService.GetSLAs();
            return Ok(slas);
        }

        [HttpGet("{slaId}")]
        public async Task<IActionResult> GetSLAById(string slaId)
        {
            var sla = await _slaService.GetSlaById(slaId);
            return Ok(sla);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSLA(CreateUpdateSlaDTO createUpdateSlaDTO)
        {
            await _slaService.CreateSLA(createUpdateSlaDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSLA(string slaId, CreateUpdateSlaDTO createUpdateSlaDTO)
        {
            await _slaService.UpdateSLA(slaId, createUpdateSlaDTO);
            return Ok(new { message = "Update successfully" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteSLA(string slaId)
        {
            await _slaService.DeleteSLA(slaId);
            return Ok(new { message = "Delete successfully" });
        }
    }
}
