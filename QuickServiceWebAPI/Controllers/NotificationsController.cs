using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {

        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Authorize]
        [HttpGet("noti")]
        public async Task<IActionResult> GetAllNotification(string userId, bool isGetOnlyUnRead)
        {
            return Ok(await _notificationService.GetNotifications(userId, isGetOnlyUnRead));
        }
    }
}
