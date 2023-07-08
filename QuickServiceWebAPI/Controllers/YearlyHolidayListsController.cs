using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.DTOs.YearHolidayListDTO;
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
        public IActionResult GetAllSLA()
        {
            var yearlyHolidayList = _yearlyHolidayListService.GetYearlyHolidayList();
            return Ok(yearlyHolidayList);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSLA(CreateUpdateYearlyHolidayListDTO createUpdateYearlyHolidayListDTO)
        {
            await _yearlyHolidayListService.CreateYearlyHoliday(createUpdateYearlyHolidayListDTO);
            return Ok(new { message = "Create successfully" });
        }
    }
}
