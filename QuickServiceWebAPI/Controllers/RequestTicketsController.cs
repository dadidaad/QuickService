using Microsoft.AspNetCore.Authorization;
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
        [Route("getalltickets/{requester}/{requestTicketId?}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTicketsForRequester(string requester, string? requestTicketId)
        {
            var requesterResquestDTO = new RequesterResquestDTO
            {
                Requester = requester,
                RequestTicketId = requestTicketId
            };
            return Ok(await _requestTicketService.GetAllListRequestTicketForRequester(requesterResquestDTO));
        }

        [HttpGet]
        [HasPermission(PermissionEnum.ManageTickets, RoleType.Agent)]
        public async Task<IActionResult> GetAllTickets()
        {
            return Ok(await _requestTicketService.GetAllListRequestTicket());
        }

        [HttpGet("get/{requestTicketId}")]
        //[HasPermission(PermissionEnum.ManageTickets, RoleType.Agent)]
        [Authorize]
        public async Task<IActionResult> GetRequestTicket(string requestTicketId)
        {
            return Ok(await _requestTicketService.GetDetailsRequestTicket(requestTicketId));
        }

        [Route("gettickets/{requester}/{requestTicketId?}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRequestTicketForRequester(string requester, string? requestTicketId)
        {
            //Requester RequestTicketId
            var requesterResquestDTO = new RequesterResquestDTO
            {
                Requester = requester,
                RequestTicketId = requestTicketId
            };
            return Ok(await _requestTicketService.GetDetailsRequestTicketForRequester(requesterResquestDTO));
        }
     
        [HttpPost("sendticket")]
        [Authorize]
        public async Task<IActionResult> SendRequestTicket(CreateRequestTicketDTO createRequestTicketDTO)
        {
            var ticketId = await _requestTicketService.SendRequestTicket(createRequestTicketDTO);
            return Ok(new { message = "Send successfully", ticketId = ticketId });
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
