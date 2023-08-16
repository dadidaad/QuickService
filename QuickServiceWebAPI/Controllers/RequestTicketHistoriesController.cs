using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestTicketHistoriesController : ControllerBase
    {
        private readonly IRequestTicketHistoryService _requestTicketHistoryService;
        public RequestTicketHistoriesController(IRequestTicketHistoryService requestTicketHistoryService)
        {
            _requestTicketHistoryService = requestTicketHistoryService;
        }

        [HttpGet("{requestTicketId}")]
        public async Task<IActionResult> GetRequestTicketHistoryByRequestTicketId(string requestTicketId)
        {
            var serviceCategories = await _requestTicketHistoryService.GetRequestTicketHistoryByRequestTicketId(requestTicketId);
            return Ok(serviceCategories);
        }
    }
}
