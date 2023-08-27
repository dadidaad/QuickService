using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.YearHolidayList;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YearlyHolidayListsController : ControllerBase
    {
        private readonly IYearlyHolidayListService _yearlyHolidayListService;
        public YearlyHolidayListsController(IYearlyHolidayListService yearlyHolidayListService)
        {
            _yearlyHolidayListService = yearlyHolidayListService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllYearHoliday()
        {
            var yearlyHolidayList = _yearlyHolidayListService.GetYearlyHolidayList();
            return Ok(yearlyHolidayList);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateYearHoliday(CreateUpdateYearlyHolidayListDTO createUpdateYearlyHolidayListDTO)
        {
            await _yearlyHolidayListService.CreateYearlyHoliday(createUpdateYearlyHolidayListDTO);
            return Ok(new { message = "Create successfully" });
        }
    }
}
