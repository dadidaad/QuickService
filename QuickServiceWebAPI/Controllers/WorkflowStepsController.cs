﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.WorkflowStep;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowStepsController : ControllerBase
    {
        private readonly IWorkflowStepService _workflowStepService;
        public WorkflowStepsController(IWorkflowStepService workflowStepService)
        {
            _workflowStepService = workflowStepService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllWorkflowStep()
        {
            var workflowSteps = _workflowStepService.GetWorkflowsStep();
            return Ok(workflowSteps);
        }

        [HttpGet("{workflowStepId}")]
        public async Task<IActionResult> GetWorkflowStepById(string workflowStepId)
        {
            var workflowStep = await _workflowStepService.GetWorkflowStepById(workflowStepId);
            return Ok(workflowStep);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWorkflowStep(CreateUpdateWorkflowStepDTO createUpdateWorkflowStepDTO)
        {
            await _workflowStepService.CreateWorkflowStep(createUpdateWorkflowStepDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateWorkflowStep(string workflowStepId, CreateUpdateWorkflowStepDTO createUpdateWorkflowStepDTO)
        {
            await _workflowStepService.UpdateWorkflowStep(workflowStepId, createUpdateWorkflowStepDTO);
            return Ok(new { message = "Update successfully" });
        }

    }
}
