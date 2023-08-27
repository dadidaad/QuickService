using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.Change;
using QuickServiceWebAPI.DTOs.Problem;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemsController : ControllerBase
    {
        private readonly IProblemService _problemService;
        public ProblemsController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllProblem()
        {
            var problems = _problemService.GetAllProblems();
            return Ok(problems);
        }

        [HttpGet("{problemId}")]
        public async Task<IActionResult> GetProblem(string problemId)
        {
            var problem = await _problemService.GetProblemById(problemId);
            return Ok(problem);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProblem(CreateProblemDTO createProblemDTO)
        {
            var createdChange = await _problemService.CreateProblem(createProblemDTO);
            return Ok(createdChange);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProblem([FromForm] UpdateProblemDTO updateProblemDTO)
        {
            await _problemService.UpdateProblem(updateProblemDTO);
            return Ok(new { message = "Update successfully", errorCode = 0 });
        }
    }
}
