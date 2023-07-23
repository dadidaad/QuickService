using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.CustomAttributes;
using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Services;

namespace QuickServiceWebAPI.Controllers
{
    [HasPermission(PermissionEnum.ManageAttachments, RoleType.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;
        public AttachmentsController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpGet("getall")]
        public IActionResult GetAllAttachment()
        {
            var attachments = _attachmentService.GetAttachments();
            return Ok(attachments);
        }

        [HttpGet("{attachmentId}")]
        public async Task<IActionResult> GetAttachmentById(string attachmentId)
        {
            var attachment = await _attachmentService.GetAttachmentById(attachmentId);
            return Ok(attachment);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAttachment(CreateUpdateAttachmentDTO createUpdateAttachmentDTO)
        {
            await _attachmentService.CreateAttachment(createUpdateAttachmentDTO);
            return Ok(new { message = "Create successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAttachment(string attachmentId, CreateUpdateAttachmentDTO createUpdateAttachmentDTO)
        {
            await _attachmentService.UpdateAttachment(attachmentId, createUpdateAttachmentDTO);
            return Ok(new { message = "Update successfully" });
        }

    }
}
