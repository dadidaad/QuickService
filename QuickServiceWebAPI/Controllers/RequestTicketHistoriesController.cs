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
            var requestTicketHistories = await _requestTicketHistoryService.GetRequestTicketHistoryByRequestTicketId(requestTicketId);
            return Ok(requestTicketHistories);
        }

        [HttpGet("change/{changeId}")]
        public async Task<IActionResult> GetRequestTicketHistoryByChangeId(string changeId)
        {
            var requestTicketHistories = await _requestTicketHistoryService.GetRequestTicketHistoryByChangeId(changeId);
            return Ok(requestTicketHistories);
        }

        [HttpGet("problem/{problemId}")]
        public async Task<IActionResult> GetRequestTicketHistoryByProblemId(string problemId)
        {
            var requestTicketHistories = await _requestTicketHistoryService.GetRequestTicketHistoryByProblemId(problemId);
            return Ok(requestTicketHistories);
        }
    }
}
