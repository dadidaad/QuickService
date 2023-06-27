using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.Services;

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
            var services = _serviceCategoryService.GetServiceCategories();
            return Ok(services);
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
