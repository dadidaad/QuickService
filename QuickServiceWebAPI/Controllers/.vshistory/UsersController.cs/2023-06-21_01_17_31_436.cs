using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly QuickServiceContext _context;
        public UsersController(QuickServiceContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            await _context.Database.ExecuteSqlRawAsync("SELECT 1");
            return Ok();
        }
    }
}
