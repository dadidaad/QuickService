﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    //[HasPermission(PermissionEnum.ManageServiceCategories, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoriesController : ControllerBase
    {
        private readonly IServiceCategoryService _serviceCategoryService;
        public ServiceCategoriesController(IServiceCategoryService serviceCategoryService)
        {
            _serviceCategoryService = serviceCategoryService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllServiceCategory()
        {
            var serviceCategories = _serviceCategoryService.GetServiceCategories();
            return Ok(serviceCategories);
        }

        [HttpGet("{serviceCategoryId}")]
        public async Task<IActionResult> GetServiceCategoryById(string serviceCategoryId)
        {
            var serviceCategory = await _serviceCategoryService.GetServiceCategoryById(serviceCategoryId);
            return Ok(serviceCategory);
        }

        [HasPermission(PermissionEnum.ManageServiceCategories, RoleType.Admin)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateServiceCategory(CreateUpdateServiceCategoryDTO createUpdateServiceCategoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }
            await _serviceCategoryService.CreateServiceCategory(createUpdateServiceCategoryDTO);
            var serviceCategory = await _serviceCategoryService.GetLastServiceCategoryWithServiceItems();
            return Ok(serviceCategory);
        }

        [HasPermission(PermissionEnum.ManageServiceCategories, RoleType.Admin)]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateServiceCategory(string serviceCategoryId, CreateUpdateServiceCategoryDTO createUpdateServiceCategoryDTO)
        {
            await _serviceCategoryService.UpdateServiceCategory(serviceCategoryId, createUpdateServiceCategoryDTO);
            var serviceCategory = await _serviceCategoryService.GetServiceCategoryById(serviceCategoryId);
            return Ok(serviceCategory);
        }

        [HasPermission(PermissionEnum.ManageServiceCategories, RoleType.Admin)]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteServiceCategory(string serviceCategoryId)
        {
            await _serviceCategoryService.DeleteServiceCategory(serviceCategoryId);
            return Ok(new { message = "Delete successfully" });
        }
    }
}
