using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.BusinessHour;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessHoursController : ControllerBase
    {
        private readonly IBusinessHourService _businessHourService;
        public BusinessHoursController(IBusinessHourService businessHourService)
        {
            _businessHourService = businessHourService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllBusinessHour()
        {
            var services = _businessHourService.GetBusinessHours();
            return Ok(services);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBusinessHour(CreateUpdateBusinessHourDTO createUpdateBusinessHourDTO)
        {
            await _businessHourService.CreateBusinessHour(createUpdateBusinessHourDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateBusinessHour(string businessHourId, CreateUpdateBusinessHourDTO createUpdateBusinessHourDTO)
        {
            await _businessHourService.UpdateBusinessHour(businessHourId, createUpdateBusinessHourDTO);
            return Ok(new { message = "Update successfully" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteBusinessHour(string businessHourId)
        {
            await _businessHourService.DeleteBusinessHour(businessHourId);
            return Ok(new { message = "Delete successfully" });
        }
    }
}
