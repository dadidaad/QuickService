using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IAttachmentRepository
    {
        public List<Attachment> GetAttachments();
        public Task<Attachment> GetAttachmentById(string attachmentId);
        public Task AddAttachment(Attachment attachment);
        public Task UpdateAttachment(Attachment attachment);
        public Task DeleteAttachment(Attachment attachment);
        public Task<Attachment> GetLastAttachment();
    }
}
