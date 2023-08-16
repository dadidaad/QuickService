using Microsoft.AspNetCore.Authorization;
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
        [HasPermission(PermissionEnum.ManageServiceItems, RoleType.Admin)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateServiceItem(CreateUpdateServiceItemDTO createUpdateServiceItemDTO)
        {
            await _serviceItemService.CreateServiceItem(createUpdateServiceItemDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HasPermission(PermissionEnum.ManageServiceItems, RoleType.Admin)]
        [HttpPut("update/{serviceItemId}")]
        public async Task<IActionResult> UpdateServiceItem(string serviceItemId, CreateUpdateServiceItemDTO createUpdateServiceItemDTO)
        {
            await _serviceItemService.UpdateServiceItem(serviceItemId, createUpdateServiceItemDTO);
            return Ok(new { message = "Update successfully" });
        }
    }
}
