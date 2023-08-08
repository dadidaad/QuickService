using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardsController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("countRequestTicket")]
        [HasPermission(PermissionEnum.ManageDashboard, RoleType.Admin)]
        public async Task<IActionResult> GetRequestTicketCount()
        {
            var requestTicketCount = await _dashboardService.GetRequestTicketCount();
            var incident = await _dashboardService.GetRequestTicketIncidentCount();
            var changeCount = await _dashboardService.GetChangeCount();
            var problemCount = await _dashboardService.GetProblemCount();
            var serviceRequests = await _dashboardService.GetRequestTicketByServiceCategoryCount();

            return Ok(new
            {
                RequestTicket = requestTicketCount,
                Incident = incident,
                Change = changeCount,
                Problem = problemCount,
                ServiceRequests  = serviceRequests
            });
        }
    }
}
