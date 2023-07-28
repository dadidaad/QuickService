using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Services
{
    public interface IAttachmentService
    {
        public List<AttachmentDTO> GetAttachments();
        public Task<AttachmentDTO> GetAttachmentById(string attachmentId);
        public Task<Attachment> CreateAttachment(IFormFile file);
        public Task UpdateAttachment(string attachmentId, CreateUpdateAttachmentDTO createUpdateAttachmentDTO);
        public Task DeleteAttachment(string attachmentId);
        public Task<string> GetNextId();
    }
}
