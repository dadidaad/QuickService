using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestTicketsController : ControllerBase
    {
        private readonly IRequestTicketService _requestTicketService;

        public RequestTicketsController(IRequestTicketService requestTicketService)
        {
            _requestTicketService = requestTicketService;
        }
        [Route("getalltickets/{requester}/{requestTicketId?}")]
        [HttpGet]
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
        public async Task<IActionResult> GetRequestTicket(string requestTicketId)
        {
            return Ok(await _requestTicketService.GetDetailsRequestTicket(requestTicketId));
        }

        [Route("gettickets/{requester}/{requestTicketId?}")]
        [HttpGet]
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
        public async Task<IActionResult> SendRequestTicket([FromForm] CreateRequestTicketDTO createRequestTicketDTO)
        {
            return Ok(new { message = "Send successfully", RequestTicketDTO = await _requestTicketService.SendRequestTicket(createRequestTicketDTO) });
        }

        [HttpPut("update")]
        [HasPermission(PermissionEnum.ManageTickets, RoleType.Agent)]
        public async Task<IActionResult> UpdateRequestTicket(UpdateRequestTicketDTO updateRequestTicketDTO)
        {
            return Ok(new
            {
                message = "Update successfully",
                UpdateRequestTicketDTO = await _requestTicketService.UpdateRequestTicket(updateRequestTicketDTO)
            });

        }

        [HttpPut("cancel/{requestTicketId}")]
        public async Task<IActionResult> CancelRequestTicketForRequester(string requestTicketId)
        {
            await _requestTicketService.CancelRequestTicket(requestTicketId);
            return Ok(new { message = "Cancel successfully" });
        }

        [HttpPut("confirm/{requestTicketId}")]
        public async Task<IActionResult> ConfirmRequestTicketForRequester(string requestTicketId)
        {
            await _requestTicketService.ConfirmRequestTicket(requestTicketId);
            return Ok(new { message = "Confirm Status successfully" });
        }

        [Route("querytickets")]
        [HttpPost]
        public async Task<IActionResult> GetTicketByQuery(QueryDTO queryDTO)
        {
            return Ok(await _requestTicketService.GetRequestTicketsQueryAdmin(queryDTO));
        }
        [Route("filtertickets")]
        [HttpPost]
        public async Task<IActionResult> GetTicketByQuery(QueryConfigDTO queryConfigDTO)
        {
            return Ok(await _requestTicketService.GetRequestTicketsFilterUser(queryConfigDTO));
        }

        [Route("getticketsadmin/{ticketType?}/{queryId?}")]
        [HttpGet]
        //[HasPermission(PermissionEnum.ManageTickets, RoleType.Admin)]
        public async Task<IActionResult> GetTicketForAdmin(string ticketType, string queryId)
        {
            return Ok(await _requestTicketService.GetRequestTicketsAdmin(ticketType, queryId));
        }



    }
}
