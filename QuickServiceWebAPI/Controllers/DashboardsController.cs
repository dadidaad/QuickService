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

        [HttpGet("countRequestTicketByChangeStatus")]
        //[HasPermission(PermissionEnum.ManageDashboard, RoleType.Admin)]
        public async Task<IActionResult> GetRequestTicketByChangeStatusCount()
        {
            var requestTicket = await _dashboardService.GetRequestTicketByChangeStatusCount();

            return Ok(new
            {
                RequestTicket = requestTicket
            });
        }

        [HttpGet("countRequestTicketByChangeChangeType")]
        //[HasPermission(PermissionEnum.ManageDashboard, RoleType.Admin)]
        public async Task<IActionResult> GetRequestTicketByChangeChangeTypeCount()
        {
            var requestTicket = await _dashboardService.GetRequestTicketByChangeChangeTypeCount();

            return Ok(new
            {
                RequestTicket = requestTicket
            });
        }

        [HttpGet("countRequestTicketByChangeImpact")]
        //[HasPermission(PermissionEnum.ManageDashboard, RoleType.Admin)]
        public async Task<IActionResult> GetRequestTicketByChangeImpactCount()
        {
            var requestTicket = await _dashboardService.GetRequestTicketByChangeImpactCount();

            return Ok(new
            {
                RequestTicket = requestTicket
            });
        }

        [HttpGet("countRequestTicketByStatus")]
        [HasPermission(PermissionEnum.ManageDashboard, RoleType.Admin)]
        public async Task<IActionResult> GetRequestTicketByStatusCount()
        {
            var requestTicket = await _dashboardService.GetRequestTicketByStatusCount();

            return Ok(new
            {
                RequestTicket = requestTicket
            });
        }

        [HttpGet("countRequestTicketByPriority")]
        [HasPermission(PermissionEnum.ManageDashboard, RoleType.Admin)]
        public async Task<IActionResult> GetRequestTicketByPriorityCount()
        {
            var requestTicket = await _dashboardService.GetRequestTicketByPriorityCount();

            return Ok(new
            {
                RequestTicket = requestTicket
            });
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
