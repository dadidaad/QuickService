using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.Services;

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
    }
}
