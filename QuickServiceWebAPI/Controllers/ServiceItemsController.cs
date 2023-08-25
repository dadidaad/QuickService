using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ServiceItemsController : ControllerBase
    {
        private readonly IServiceItemService _serviceItemService;
        public ServiceItemsController(IServiceItemService serviceItemService)
        {
            _serviceItemService = serviceItemService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllServiceItem()
        {
            var serviceItems = _serviceItemService.GetServiceItems();
            return Ok(serviceItems);
        }

        [HttpGet("{serviceItemId}")]
        public async Task<IActionResult> GetServiceItemById(string serviceItemId)
        {
            var serviceItem = await _serviceItemService.GetServiceItemById(serviceItemId);
            return Ok(serviceItem);
        }
        [HasPermission(PermissionEnum.ManageServiceItems, RoleType.Admin)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateServiceItem(CreateUpdateServiceItemDTO createUpdateServiceItemDTO)
        {
            var serviceItemDto = await _serviceItemService.CreateServiceItem(createUpdateServiceItemDTO);
            return Ok(new { message = "Create successfully", ServiceItemDTO = serviceItemDto });
        }

        [HasPermission(PermissionEnum.ManageServiceItems, RoleType.Admin)]
        [HttpPut("update/{serviceItemId}")]
        public async Task<IActionResult> UpdateServiceItem(string serviceItemId, CreateUpdateServiceItemDTO createUpdateServiceItemDTO)
        {
            var serviceItemDto = await _serviceItemService.UpdateServiceItem(serviceItemId, createUpdateServiceItemDTO);
            return Ok(new { message = "Update successfully", ServiceItemDTO = serviceItemDto });
        }
    }
}
