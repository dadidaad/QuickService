﻿using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowsController : ControllerBase
    {
        private readonly IWorkflowService _workflowService;

        public WorkflowsController(IWorkflowService workflowService)
        {
            _workflowService = workflowService;
        }
        //[HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllWorkflow()
        {
            var workflows = await _workflowService.GetWorkflows();
            return Ok(workflows);
        }

        //[HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        [HttpGet("{workflowId}")]
        public async Task<IActionResult> GetWorkflowById(string workflowId)
        {
            var workflow = await _workflowService.GetWorkflowById(workflowId);
            return Ok(workflow);
        }

        [HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateWorkflow(CreateUpdateWorkflowDTO createUpdateWorkflowDTO)
        {
            return Ok(new { message = "Create successfully", WorkflowDTO = await _workflowService.CreateWorkflow(createUpdateWorkflowDTO) });
        }

        [HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateWorkflow(string workflowId, CreateUpdateWorkflowDTO createUpdateWorkflowDTO)
        {
            await _workflowService.UpdateWorkflow(workflowId, createUpdateWorkflowDTO);
            return Ok(new { message = "Update successfully" });
        }
        [HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        [HttpDelete("toggle/{workflowId}")]
        public async Task<IActionResult> ToggleWorkflow(string workflowId)
        {
            await _workflowService.ToggleStatusWorkflow(workflowId);
            return Ok(new { message = "Toggle successfully" });
        }

        [HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        [HttpPut("assign")]
        public async Task<IActionResult> AssignWorkflow(AssignWorkflowDTO assignWorkflowDTO)
        {
            await _workflowService.AssignWorkflow(assignWorkflowDTO);
            return Ok(new { message = "Assign successfully" });
        }

        [HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        [HttpPut("remove")]
        public async Task<IActionResult> RemoveWorkflowFromServiceItem(RemoveWorkflowFromServiceItemDTO removeWorkflowFromServiceItemDTO)
        {
            await _workflowService.RemoveWorkflowFromServiceItem(removeWorkflowFromServiceItemDTO);
            return Ok(new { message = "Remove successfully" });
        }


        //[HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        [HttpGet("checkedit/{workflowId}")]
        public async Task<IActionResult> CheckEditWorkflow(string workflowId)
        {
            return Ok(new { Condition = await _workflowService.CheckStatusRequestTicketToEditWorkflowTask(workflowId) });
        }

        [HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        [HttpPost("clone/{workflowId}")]
        public async Task<IActionResult> CloneWorkflow(string workflowId)
        {
            return Ok(new { message = "Clone successfully", WorkflowDTO = await _workflowService.CloneWorkflow(workflowId) });
        }

    }
}
