using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class QueriesController : ControllerBase
    {
        private readonly IQueryService _queryService;
        public QueriesController(IQueryService queryService)
        {
            _queryService = queryService;
        }
        
        [HttpGet("getforuser/{userId}")]
        public async Task<IActionResult> GetAllTicketsForRequester(string userIdd)
        {
            return Ok(await _queryService.GetQueryForUser(userIdd));
        }
        [HttpGet("getall")]
        public IActionResult GetQueryRequestTicket([FromQuery] QueryConfigDTO query)
        {
            var requestTickets = _queryService.GetQueryRequestTicket(query);
            return Ok(requestTickets);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateQuery(QueryDTO queryDTO)
        {
            return Ok(new { message = "Send successfully", QueryDTO = await _queryService.CreateQuery(queryDTO) });
        }
    }
}
