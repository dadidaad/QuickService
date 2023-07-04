using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;

namespace QuickServiceWebAPI.Controllers
{
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
