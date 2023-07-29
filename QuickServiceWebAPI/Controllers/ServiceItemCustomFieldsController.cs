using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.ServiceItemCustomField;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [HasPermission(PermissionEnum.ManageServiceItems, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceItemCustomFieldsController : ControllerBase
    {
        private readonly IServiceItemCustomFieldService _serviceItemCustomFieldService;

        public ServiceItemCustomFieldsController(IServiceItemCustomFieldService serviceItemCustomFieldService)
        {
            _serviceItemCustomFieldService = serviceItemCustomFieldService;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignServiceItemCustomField(CreateUpdateServiceItemCustomFieldDTO createUpdateServiceItemCustomFieldDTO)
        {
            await _serviceItemCustomFieldService.AssignServiceItemCustomField(createUpdateServiceItemCustomFieldDTO);
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
    }
}
