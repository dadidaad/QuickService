using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.DTOs.ServiceDeskHour;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceDeskHoursController : ControllerBase
    {
        private readonly IServiceDeskHourService _serviceDeskHourService;
        public ServiceDeskHoursController(IServiceDeskHourService serviceDeskHourService)
        {
            _serviceDeskHourService = serviceDeskHourService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllServiceDeskHour()
        {
            var serviceDeskHours = _serviceDeskHourService.GetServiceDeskHours();
            return Ok(serviceDeskHours);
        }

        [HttpGet("{serviceDeskHourId}")]
        public async Task<IActionResult> GetServiceDeskHourById(string serviceDeskHourId)
        {
            var serviceDeskHour = await _serviceDeskHourService.GetServiceDeskHourById(serviceDeskHourId);
            return Ok(serviceDeskHour);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateServiceDeskHour(CreateUpdateServiceDeskHourDTO createUpdateServiceDeskHourDTO)
        {
            await _serviceDeskHourService.CreateServiceDeskHour(createUpdateServiceDeskHourDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateServiceDeskHour(string serviceDeskHourId, CreateUpdateServiceDeskHourDTO createUpdateServiceDeskHourDTO)
        {
            await _serviceDeskHourService.UpdateServiceDeskHour(serviceDeskHourId, createUpdateServiceDeskHourDTO);
            return Ok(new { message = "Update successfully" });
        }

    }
}
