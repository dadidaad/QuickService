using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.Services;

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
