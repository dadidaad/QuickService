using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.WorkflowTransition;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowTransitionsController : ControllerBase
    {
        private readonly IWorkflowTransitionService _workflowTransitionService;

        public WorkflowTransitionsController(IWorkflowTransitionService workflowTransitionService)
        {
            _workflowTransitionService = workflowTransitionService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWorkflowTransition(CreateWorkflowTransitionDTO createWorkflowTransitionDTO)
        {
            await _workflowTransitionService.CreateWorkflowTransition(createWorkflowTransitionDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteWorkflowTransition(DeleteWorkflowTransitionDTO deleteWorkflowTransitionDTO)
        {
            await _workflowTransitionService.DeleteWorkflowTransition(deleteWorkflowTransitionDTO);
            return Ok(new { message = "Delete successfully" });
        }

        [HttpGet("getall/{workflowId}")]
        public async Task<IActionResult> GetAllTransitionByWorkflow(string workflowId)
        {
            return Ok(await _workflowTransitionService.GetWorkflowTransitionsByWorkflow(workflowId));
        }

        [HttpGet("get/{fromWorkflowTaskId}")]
        public async Task<IActionResult> GetTransitionsOfWorkflowTask(string fromWorkflowTaskId)
        {
            return Ok(await _workflowTransitionService.GetWorkflowTransitionByFromTaskId(fromWorkflowTaskId));
        }
    }
}
