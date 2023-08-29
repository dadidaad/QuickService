using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.Dashboard;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

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

        [HttpGet("countRequestTicketByProblemStatus")]
        public async Task<IActionResult> GetRequestTicketByProblemStatusCount()
        {
            var requestTicket = await _dashboardService.GetRequestTicketByProblemStatusCount();

            return Ok(new
            {
                RequestTicket = requestTicket
            });
        }

        [HttpGet("countRequestTicketByProblemPriority")]
        public async Task<IActionResult> GetRequestTicketByProblemPriorityCount()
        {
            var requestTicket = await _dashboardService.GetRequestTicketByProblemPriorityCount();

            return Ok(new
            {
                RequestTicket = requestTicket
            });
        }

        [HttpGet("countRequestTicketByProblemImpact")]
        public async Task<IActionResult> GetRequestTicketByProblemImpactCount()
        {
            var requestTicket = await _dashboardService.GetRequestTicketByProblemImpactCount();

            return Ok(new
            {
                RequestTicket = requestTicket
            });
        }

        [HttpGet("countRequestTicketByChangeStatus")]
        public async Task<IActionResult> GetRequestTicketByChangeStatusCount()
        {
            var requestTicket = await _dashboardService.GetRequestTicketByChangeStatusCount();

            return Ok(new
            {
                RequestTicket = requestTicket
            });
        }

        [HttpGet("countRequestTicketByChangeImpact")]
        public async Task<IActionResult> GetRequestTicketByChangeImpactCount()
        {
            var requestTicket = await _dashboardService.GetRequestTicketByChangeImpactCount();

            return Ok(new
            {
                RequestTicket = requestTicket
            });
        }

        [HttpGet("countRequestTicketByStatus")]
        public async Task<IActionResult> GetRequestTicketByStatusCount()
        {
            var requestTicket = await _dashboardService.GetRequestTicketByStatusCount();

            return Ok(new
            {
                RequestTicket = requestTicket
            });
        }

        [HttpGet("countRequestTicketByPriority")]
        public async Task<IActionResult> GetRequestTicketByPriorityCount()
        {
            var requestTicket = await _dashboardService.GetRequestTicketByPriorityCount();

            return Ok(new
            {
                RequestTicket = requestTicket
            });
        }

        [HttpGet("countRequestTicket")]
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
                ServiceRequests = serviceRequests
            });
        }

        [HttpPost("countbyday")]
        public async Task<IActionResult> CountRequestTicketByDay(CountByDayDTO countByDayDTO)
        {
            return Ok(await _dashboardService.CountRequestTicketByDay(countByDayDTO));
        }
    }
}
