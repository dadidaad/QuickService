using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [HasPermission(PermissionEnum.ManageSlametrics, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class SlametricsController : ControllerBase
    {
        private readonly ISlametricService _slametricService;
        public SlametricsController(ISlametricService slametricService)
        {
            _slametricService = slametricService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllSLAmetric()
        {
            var slametrics = _slametricService.GetSLAmetrics();
            return Ok(slametrics);
        }

        [HttpGet("{slametricId}")]
        public async Task<IActionResult> GetSLAById(string slametricId)
        {
            var slametric = await _slametricService.GetSLAmetricById(slametricId);
            return Ok(slametric);
        }

        //[HttpPost("create")]
        //public async Task<IActionResult> CreateSLAmetric(CreateSlametricDTO createSlametricDTO)
        //{
        //    await _slametricService.CreateSLAmetrics(createSlametricDTO);
        //    return Ok(new { message = "Create successfully" });
        //}

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSLAmetric(UpdateSlametricsDTO updateSlametricsDTO)
        {
            await _slametricService.UpdateSLAmetric(updateSlametricsDTO);
            return Ok(new { message = "Update successfully" });
        }

    }
}
