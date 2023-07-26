using QuickServiceWebAPI.DTOs.Attachment;

namespace QuickServiceWebAPI.Services
{
    public interface IAttachmentService
    {
        public List<AttachmentDTO> GetAttachments();
        public Task<AttachmentDTO> GetAttachmentById(string attachmentId);
        public Task CreateAttachment(CreateAttachmentDTO createAttachmentDTO);
        public Task UpdateAttachment(string attachmentId, CreateUpdateAttachmentDTO createUpdateAttachmentDTO);
        public Task DeleteAttachment(string attachmentId);
        public Task<string> GetNextId();
    }
}
