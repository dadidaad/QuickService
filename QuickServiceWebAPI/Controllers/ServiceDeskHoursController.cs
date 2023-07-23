using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.ServiceDeskHour;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [HasPermission(PermissionEnum.ManageServiceDeskHours, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceDeskHoursController : ControllerBase
    {
        private readonly IServiceDeskHourService _serviceDeskHourService;
        public ServiceDeskHoursController(IServiceDeskHourService serviceDeskHourService)
        {
            _serviceDeskHourService = serviceDeskHourService;
        }

        [AllowAnonymous]
        [HttpGet("getall")]
        public IActionResult GetAllServiceDeskHour()
        {
            var serviceDeskHours = _serviceDeskHourService.GetServiceDeskHours();
            return Ok(serviceDeskHours);
        }

        [AllowAnonymous]
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
