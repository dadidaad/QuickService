using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateSLAmetric(CreateUpdateSlametricDTO createUpdateSlametricDTO)
        {
            await _slametricService.CreateSLAmetric(createUpdateSlametricDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSLAmetric(string slametricId, CreateUpdateSlametricDTO createUpdateSlametricDTO)
        {
            await _slametricService.UpdateSLAmetric(slametricId, createUpdateSlametricDTO);
            return Ok(new { message = "Update successfully" });
        }

    }
}
