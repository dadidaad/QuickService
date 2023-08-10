using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.WorkflowTask;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowStepsController : ControllerBase
    {
        //private readonly IWorkflowTaskService _workflowStepService;
        //public WorkflowStepsController(IWorkflowTaskService workflowStepService)
        //{
        //    _workflowStepService = workflowStepService;
        //}

        //[Authorize]
        //[HttpGet("getall")]
        //public IActionResult GetAllWorkflowStep()
        //{
        //    var workflowSteps = _workflowStepService.GetWorkflowsStep();
        //    return Ok(workflowSteps);
        //}

        //[Authorize]
        //[HttpGet("{workflowStepId}")]
        //public async Task<IActionResult> GetWorkflowStepById(string workflowStepId)
        //{
        //    var workflowStep = await _workflowStepService.GetWorkflowStepById(workflowStepId);
        //    return Ok(workflowStep);
        //}
        //[HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        //[HttpPost("create")]
        //public async Task<IActionResult> CreateWorkflowStep(CreateUpdateWorkflowTaskDTO createUpdateWorkflowStepDTO)
        //{
        //    await _workflowStepService.CreateWorkflowStep(createUpdateWorkflowStepDTO);
        //    return Ok(new { message = "Create successfully" });
        //}
        //[HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        //[HttpPut("update")]
        //public async Task<IActionResult> UpdateWorkflowStep(string workflowStepId, CreateUpdateWorkflowTaskDTO createUpdateWorkflowStepDTO)
        //{
        //    await _workflowStepService.UpdateWorkflowStep(workflowStepId, createUpdateWorkflowStepDTO);
        //    return Ok(new { message = "Update successfully" });
        //}

        //[HasPermission(PermissionEnum.ManageWorkflows, RoleType.Admin)]
        //[HttpDelete("delete")]
        //public async Task<IActionResult> DeleteWorkflowStep(string workflowStepId)
        //{
        //    await _workflowStepService.DeleteWorkflowStep(workflowStepId);
        //    return Ok(new { message = "Delete successfully" });
        //}

    }
}
