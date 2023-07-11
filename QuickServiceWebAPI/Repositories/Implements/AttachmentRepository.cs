using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<AttachmentRepository> _logger;
        public AttachmentRepository(QuickServiceContext context, ILogger<AttachmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddAttachment(Attachment attachment)
        {
            try
            {
                _context.Attachments.Add(attachment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Attachment> GetAttachmentById(string attachmentId)
        {
            try
            {
                Attachment attachment = await _context.Attachments.FirstOrDefaultAsync(x => x.AttachmentId == attachmentId);
                return attachment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<Attachment> GetAttachments()
        {
            try
            {
                return _context.Attachments.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateAttachment(Attachment attachment)
        {
            try
            {
                _context.Attachments.Update(attachment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteAttachment(Attachment attachment)
        {
            try
            {
                _context.Attachments.Remove(attachment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Attachment> GetLastAttachment()
        {
            try
            {
                return await _context.Attachments.OrderByDescending(u => u.AttachmentId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
