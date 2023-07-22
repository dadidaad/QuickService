using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services.Authentication;

namespace QuickServiceWebAPI.Controllers
{
    [HasPermission(PermissionEnum.ManageServiceItems, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceItemCustomFieldsController : ControllerBase
    {


    }
}
