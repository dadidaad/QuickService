﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.DTOs.YearHolidayList;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Authentication;

namespace QuickServiceWebAPI.Controllers
{
    [HasPermission(PermissionEnum.ManageYearlyHolidayList, RoleType.Admin)]
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
