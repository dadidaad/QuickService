﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.Service;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [AllowAnonymous]
        [HttpGet("getall")]
        public IActionResult GetAllService()
        {
            var services = _serviceService.GetServices();
            return Ok(services);
        }

        [AllowAnonymous]
        [HttpGet("{serviceId}")]
        public async Task<IActionResult> GetServiceById(string serviceId)
        {
            var service = await _serviceService.GetServiceById(serviceId);
            return Ok(service);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateService(CreateUpdateServiceDTO createUpdateServiceDTO)
        {
            await _serviceService.CreateService(createUpdateServiceDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateService(string serviceId, CreateUpdateServiceDTO createUpdateServiceDTO)
        {
            await _serviceService.UpdateService(serviceId, createUpdateServiceDTO);
            return Ok(new { message = "Update successfully" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteService(string serviceId)
        {
            await _serviceService.DeleteService(serviceId);
            return Ok(new { message = "Delete successfully" });
        }
    }
}
