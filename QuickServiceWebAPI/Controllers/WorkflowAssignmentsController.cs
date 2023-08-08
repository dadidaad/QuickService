using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
