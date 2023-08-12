using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.WorkflowAssignment;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowAssignmentsController : ControllerBase
    {
        private readonly IWorkflowAssignmentService _workflowAssignmentService;

        public WorkflowAssignmentsController(IWorkflowAssignmentService workflowAssignmentService)
        {
            _workflowAssignmentService = workflowAssignmentService;
        }

        //[HasPermission(PermissionEnum.ManageTickets, RoleType.Agent)]
        [HttpPost("complete")]
        public async Task<IActionResult> CompleteWorkflowTask([FromForm]CheckWorkflowAssignmentDTO checkWorkflowAssignmentDTO)
        {
            await _workflowAssignmentService.CompleteWorkflowTask(checkWorkflowAssignmentDTO);
            return Ok(new { message = "Update successfully" });
        }

        [HasPermission(PermissionEnum.ManageTickets, RoleType.Agent)]
        [HttpGet("get")]
        public async Task<IActionResult> GetWorkflowTaskForRequestTicket(string requestTicketId)
        {
            return Ok(await _workflowAssignmentService.GetWorkflowAssignmentsForTicket(requestTicketId));
        }
    }
}
