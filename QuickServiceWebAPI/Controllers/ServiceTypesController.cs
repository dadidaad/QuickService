using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.ServiceType;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : ControllerBase
    {
        private readonly IServiceTypeService _serviceTypeService;
        public ServiceTypesController(IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllServiceType()
        {
            var serviceTypes = _serviceTypeService.GetServiceTypes();
            return Ok(serviceTypes);
        }

        [HttpGet("{serviceTypeId}")]
        public async Task<IActionResult> GetServiceTypeById(string serviceTypeId)
        {
            var serviceType = await _serviceTypeService.GetServiceTypeById(serviceTypeId);
            return Ok(serviceType);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateServiceType(CreateUpdateServiceTypeDTO createUpdateServiceTypeDTO)
        {
            await _serviceTypeService.CreateServiceType(createUpdateServiceTypeDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateServiceType(string serviceTypeId, CreateUpdateServiceTypeDTO createUpdateServiceTypeDTO)
        {
            await _serviceTypeService.UpdateServiceType(serviceTypeId, createUpdateServiceTypeDTO);
            return Ok(new { message = "Update successfully" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteServiceType(string serviceTypeId)
        {
            await _serviceTypeService.DeleteServiceType(serviceTypeId);
            return Ok(new { message = "Delete successfully" });
        }
    }
}
