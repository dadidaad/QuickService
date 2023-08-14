using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    //[HasPermission(PermissionEnum.ManageServiceItems, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceItemsController : ControllerBase
    {
        private readonly IServiceItemService _serviceItemService;
        public ServiceItemsController(IServiceItemService serviceItemService)
        {
            _serviceItemService = serviceItemService;
        }

        [AllowAnonymous]
        [HttpGet("getall")]
        public IActionResult GetAllServiceItem()
        {
            var serviceItems = _serviceItemService.GetServiceItems();
            return Ok(serviceItems);
        }

        [AllowAnonymous]
        [HttpGet("{serviceItemId}")]
        public async Task<IActionResult> GetServiceItemById(string serviceItemId)
        {
            var serviceItem = await _serviceItemService.GetServiceItemById(serviceItemId);
            return Ok(serviceItem);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateServiceItem(CreateUpdateServiceItemDTO createUpdateServiceItemDTO)
        {
            var serviceItemDto = await _serviceItemService.CreateServiceItem(createUpdateServiceItemDTO);
            return Ok(new { message = "Create successfully", ServiceItemDTO = serviceItemDto });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateServiceItem(string serviceItemId, CreateUpdateServiceItemDTO createUpdateServiceItemDTO)
        {
            await _serviceItemService.UpdateServiceItem(serviceItemId, createUpdateServiceItemDTO);
            return Ok(new { message = "Update successfully" });
        }
    }
}
