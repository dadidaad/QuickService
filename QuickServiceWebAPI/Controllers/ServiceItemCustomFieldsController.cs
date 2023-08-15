using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.ServiceItemCustomField;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceItemCustomFieldsController : ControllerBase
    {
        private readonly IServiceItemCustomFieldService _serviceItemCustomFieldService;

        public ServiceItemCustomFieldsController(IServiceItemCustomFieldService serviceItemCustomFieldService)
        {
            _serviceItemCustomFieldService = serviceItemCustomFieldService;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignServiceItemCustomField(List<CreateUpdateServiceItemCustomFieldDTO> createUpdateServiceItemCustomFieldDTOs)
        {
            await _serviceItemCustomFieldService.AssignServiceItemCustomField(createUpdateServiceItemCustomFieldDTOs);
            return Ok(new { message = "Assign successfully" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteServiceItemCustomField(DeleteServiceItemCustomFieldDTO deleteServiceItemCustomFieldDTO)
        {
            await _serviceItemCustomFieldService.DeleteServiceItemCustomField(deleteServiceItemCustomFieldDTO);
            return Ok(new { message = "Delete successfully" });
        }

        [HttpGet("getbyserviceitem/{serviceItemId}")]
        public async Task<IActionResult> GetServiceItemCustomFieldByServiceItem(string serviceItemId)
        {
            return Ok(await _serviceItemCustomFieldService.GetCustomFieldByServiceItem(serviceItemId));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateServiceItem(List<CreateUpdateServiceItemCustomFieldDTO> createUpdateServiceItemCustomFieldDTOs)
        {
            await _serviceItemCustomFieldService.UpdateServiceItemCustomField(createUpdateServiceItemCustomFieldDTOs);
            return Ok(new { message = "Update successfully" });
        }
    }
}
