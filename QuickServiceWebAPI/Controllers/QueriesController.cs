using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly IQueryService _queryService;
        public QueriesController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        [HttpGet("getall")]
        public IActionResult GetQueryRequestTicket(string? assignee, DateTime? createFrom, DateTime? createTo, string? description,
                                                         string? group, string? requester, string? requestType, string? priority, string? status)
        {
            var requestTickets = _queryService.GetQueryRequestTicket(assignee, createFrom, createTo, description, group, requester, requestType, priority, status);
            return Ok(requestTickets);
        }
    }
}
