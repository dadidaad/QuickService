using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [HasPermission(PermissionEnum.ManageServiceCategories, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoriesController : ControllerBase
    {
        private readonly IServiceCategoryService _serviceCategoryService;
        public ServiceCategoriesController(IServiceCategoryService serviceCategoryService)
        {
            _serviceCategoryService = serviceCategoryService;
        }

        [AllowAnonymous]
        [HttpGet("getall")]
        public IActionResult GetAllServiceCategory()
        {
            var serviceCategories = _serviceCategoryService.GetServiceCategories();
            return Ok(serviceCategories);
        }

        [AllowAnonymous]
        [HttpGet("getallwithserviceitems")]
        public IActionResult GetAllServiceCategoryWithServiceItems()
        {
            var serviceCategories = _serviceCategoryService.GetServiceCategoriesWithServiceItems();
            return Ok(serviceCategories);
        }

        [AllowAnonymous]
        [HttpGet("getwithserviceitems/{serviceCategoryId}")]
        public async Task<IActionResult> GetServiceCategoryByIdWithServiceItems(string serviceCategoryId)
        {
            var serviceCategory = await _serviceCategoryService.GetServiceCategoryByIdWithServiceItems(serviceCategoryId);
            return Ok(serviceCategory);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateServiceCategory(CreateUpdateServiceCategoryDTO createUpdateServiceCategoryDTO)
        {
            await _serviceCategoryService.CreateServiceCategory(createUpdateServiceCategoryDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateServiceCategory(string serviceCategoryId, CreateUpdateServiceCategoryDTO createUpdateServiceCategoryDTO)
        {
            await _serviceCategoryService.UpdateServiceCategory(serviceCategoryId, createUpdateServiceCategoryDTO);
            return Ok(new { message = "Update successfully" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteServiceCategory(string serviceCategoryId)
        {
            await _serviceCategoryService.DeleteServiceCategory(serviceCategoryId);
            return Ok(new { message = "Delete successfully" });
        }
    }
}
