﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.WorkflowTask;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowTasksController : ControllerBase
    {
        private readonly IWorkflowTaskService _workflowTaskService;
        public WorkflowTasksController(IWorkflowTaskService workflowTaskService)
        {
            _workflowTaskService = workflowTaskService;
        }

        [HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]

        [HttpGet("{workflowStepId}")]
        public async Task<IActionResult> GetWorkflowTaskById(string workflowStepId)
        {
            var workflowStep = await _workflowTaskService.GetWorkflowTaskById(workflowStepId);
            return Ok(workflowStep);
        }
        [HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateWorkflowTask(CreateUpdateWorkflowTaskDTO createUpdateWorkflowTaskDTO)
        {
            await _workflowTaskService.CreateWorkflowTask(createUpdateWorkflowTaskDTO);
            return Ok(new { message = "Create successfully" });
        }
        [HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateWorkflowTask(string workflowTaskId, CreateUpdateWorkflowTaskDTO createUpdateWorkflowTaskDTO)
        {
            await _workflowTaskService.UpdateWorkflowTask(workflowTaskId, createUpdateWorkflowTaskDTO);
            return Ok(new { message = "Update successfully" });
        }

        [HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteWorkflowTask(string workflowTaskId)
        {
            await _workflowTaskService.DeleteWorkflowTask(workflowTaskId);
            return Ok(new { message = "Delete successfully" });
        }

    }
}