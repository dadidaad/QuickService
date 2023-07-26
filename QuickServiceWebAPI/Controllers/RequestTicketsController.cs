using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestTicketsController : ControllerBase
    {
        private readonly IRequestTicketService _requestTicketService;

        public RequestTicketsController(IRequestTicketService requestTicketService)
        {
            _requestTicketService = requestTicketService;
        }

        [HttpGet("getalltickets")]
        [Authorize]
        public async Task<IActionResult> GetAllTicketsForRequester(RequesterResquestDTO requesterResquestDTO)
        {
            return Ok(await _requestTicketService.GetAllListRequestTicketForRequester(requesterResquestDTO));
        }

        [HttpGet]
        [HasPermission(PermissionEnum.ManageTickets, RoleType.Agent)]
        public async Task<IActionResult> GetAllTickets()
        {
            return Ok(await _requestTicketService.GetAllListRequestTicket());
        }

        [HttpGet("get/{requestTicketId}")]
        [HasPermission(PermissionEnum.ManageTickets, RoleType.Agent)]
        public async Task<IActionResult> GetRequestTicket(string requestTicketId)
        {
            return Ok(await _requestTicketService.GetDetailsRequestTicket(requestTicketId));
        }

        [HttpGet("gettickets")]
        [Authorize]
        public async Task<IActionResult> GetRequestTicketForRequester(RequesterResquestDTO requesterResquestDTO)
        {
            return Ok(await _requestTicketService.GetDetailsRequestTicketForRequester(requesterResquestDTO));
        }

        [HttpPost("sendticket")]
        [Authorize]
        public async Task<IActionResult> SendRequestTicket(CreateRequestTicketDTO createRequestTicketDTO)
        {
            await _requestTicketService.SendRequestTicket(createRequestTicketDTO);
            return Ok(new { message = "Send successfully" });
        }

        [HttpPut("update")]
        [HasPermission(PermissionEnum.ManageTickets, RoleType.Agent)]
        public async Task<IActionResult> UpdateRequestTicket(UpdateRequestTicketDTO updateRequestTicketDTO)
        {
            await _requestTicketService.UpdateRequestTicket(updateRequestTicketDTO);
            return Ok(new { message = "Update successfully" });

        }
    }
}
