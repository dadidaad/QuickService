using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.CustomField;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Authentication;

namespace QuickServiceWebAPI.Controllers
{
    [HasPermission(PermissionEnum.ManageCustomFields, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomFieldsController : ControllerBase
    {
        private readonly ICustomFieldService _customFieldService;
        public CustomFieldsController(ICustomFieldService customFieldService)
        {
            _customFieldService = customFieldService;
        }

        [AllowAnonymous]
        [HttpGet("getall")]
        public IActionResult GetAllCustomField()
        {
            var customFields = _customFieldService.GetCustomFields();
            return Ok(customFields);
        }

        [AllowAnonymous]
        [HttpGet("{customFieldId}")]
        public async Task<IActionResult> GetCustomField(string customFieldId)
        {
            var customField = await _customFieldService.GetCustomField(customFieldId);
            return Ok(customField);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomField(CreateUpdateCustomFieldDTO createUpdateCustomField)
        {
            await _customFieldService.CreateCustomField(createUpdateCustomField);
            return Ok(new { message = "Create successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateServiceItem(string customFieldId, CreateUpdateCustomFieldDTO createUpdateCustomField)
        {
            await _customFieldService.UpdateCustomField(customFieldId, createUpdateCustomField);
            return Ok(new { message = "Update successfully" });
        }
    }
}
