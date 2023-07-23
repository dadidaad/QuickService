using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Authentication;
using QuickServiceWebAPI.Services.Implements;

namespace QuickServiceWebAPI.Controllers
{
    [HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowsController : ControllerBase
    {
        private readonly IWorkflowService _workflowService;
        public WorkflowsController(IWorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllWorkflow()
        {
            var workflows = _workflowService.GetWorkflows();
            return Ok(workflows);
        }

        [HttpGet("{workflowId}")]
        public async Task<IActionResult> GetWorkflowById(string workflowId)
        {
            var workflow = await _workflowService.GetWorkflowById(workflowId);
            return Ok(workflow);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWorkflow(CreateUpdateWorkflowDTO createUpdateWorkflowDTO)
        {
            await _workflowService.CreateWorkflow(createUpdateWorkflowDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateWorkflow(string workflowId, CreateUpdateWorkflowDTO createUpdateWorkflowDTO)
        {
            await _workflowService.UpdateWorkflow(workflowId, createUpdateWorkflowDTO);
            return Ok(new { message = "Update successfully" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteWorkflow(string workflowId)
        {
            await _workflowService.DeleteWorkflow(workflowId);
            return Ok(new { message = "Delete successfully" });
        }
    }
}
