using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.RequestTicketExt;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestTicketExtsController : ControllerBase
    {
        private readonly IRequestTicketExtService _requestTicketExtService;
        public RequestTicketExtsController(IRequestTicketExtService requestTicketExtService)
        {
            _requestTicketExtService = requestTicketExtService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllRequestTicketExt()
        {
            var requestTicketExts = _requestTicketExtService.GetRequestTicketExts();
            return Ok(requestTicketExts);
        }

        [HttpGet("{requestTicketExtId}")]
        public async Task<IActionResult> GetRequestTicketExtById(string requestTicketExtId)
        {
            var requestTicketExt = await _requestTicketExtService.GetRequestTicketExtById(requestTicketExtId);
            return Ok(requestTicketExt);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRequestTicketExt([FromBody] List<CreateUpdateRequestTicketExtDTO> createUpdateRequestTicketExtDTOs)
        {
            await _requestTicketExtService.CreateRequestTicketExt(createUpdateRequestTicketExtDTOs);
            return Ok(new { message = "Create successfully" });
        }



        [HttpPut("update")]
        public async Task<IActionResult> UpdateRequestTicketExt(string requestTicketExtId, CreateUpdateRequestTicketExtDTO createUpdateRequestTicketExtDTO)
        {
            await _requestTicketExtService.UpdateRequestTicketExt(requestTicketExtId, createUpdateRequestTicketExtDTO);
            return Ok(new { message = "Update successfully" });
        }
    }
}
