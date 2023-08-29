using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.Services;

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
        [HttpGet("getdetail/{Id}")]
        public async Task<IActionResult> GetQueryById(string Id)
        {
            return Ok(await _queryService.GetQueryById(Id));
        }

        [HttpGet("getforuser/{userId}/{type}")]
        public async Task<IActionResult> GetQueryForUser(string userId, string type)
        {
            return Ok(await _queryService.GetQueryForUser(userId, type));
        }
        [HttpGet("getforuser/{userId}")]
        public async Task<IActionResult> GetQueryForUser(string userId)
        {
            return Ok(await _queryService.GetQueryForUser(userId, ""));
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
        [HttpPut("update")]
        public async Task<IActionResult> UpdateQuery(QueryDTO queryDTO)
        {
            return Ok(new { message = "Send successfully", QueryDTO = await _queryService.UpdateQuery(queryDTO) });
        }
    }
}
