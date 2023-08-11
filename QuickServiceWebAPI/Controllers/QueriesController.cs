using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.Query;
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
        public IActionResult GetQueryRequestTicket([FromQuery] QueryDTO query)
        {
            var requestTickets = _queryService.GetQueryRequestTicket(query);
            return Ok(requestTickets);
        }
    }
}
