﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.DTOs.ServiceItem;
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateServiceItem(CreateUpdateServiceItemDTO createUpdateServiceItemDTO)
        {
            await _serviceItemService.CreateServiceItem(createUpdateServiceItemDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateServiceItem(string serviceItemId, CreateUpdateServiceItemDTO createUpdateServiceItemDTO)
        {
            await _serviceItemService.UpdateServiceItem(serviceItemId, createUpdateServiceItemDTO);
            return Ok(new { message = "Update successfully" });
        }
    }
}