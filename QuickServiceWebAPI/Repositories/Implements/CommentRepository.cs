using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class CommentRepository : ICommentRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<CommentRepository> _logger;
        public CommentRepository(QuickServiceContext context, ILogger<CommentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddComment(Comment comment)
        {
            try
            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Comment> GetCommentById(string commentId)
        {
            try
            {
                Comment comment = await _context.Comments.AsNoTracking().Include(a => a.Attachment).Include(u => u.CommentByNavigation)
                                 .Include(r => r.RequestTicket).FirstOrDefaultAsync(x => x.CommentId == commentId);
                return comment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<Comment> GetCommentByUser(string userId)
        {
            try
            {
                return _context.Comments.Include(a => a.Attachment).Include(u => u.CommentByNavigation)
                                 .Include(r => r.RequestTicket).Where(x => x.CommentBy == userId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<Comment> GetCommentsByRequestTicket(string requestTicketId)
        {
            try
            {
                return _context.Comments.AsQueryable().Include(a => a.Attachment).Include(u => u.CommentByNavigation)
                                 .Include(r => r.RequestTicket).ThenInclude(sla => sla.Sla).ThenInclude(slm => slm.Slametrics)
                                 .Where(x => x.RequestTicketId == requestTicketId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<Comment> GetCustomerCommentsByRequestTicket(string requestTicketId)
        {
            try
            {
                return _context.Comments.AsQueryable().Include(a => a.Attachment).Include(u => u.CommentByNavigation)
                                 .Include(r => r.RequestTicket).Where(x => x.RequestTicketId == requestTicketId && x.IsInternal == false).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateComment(Comment comment)
        {
            try
            {
                _context.Comments.Update(comment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteComment(Comment comment)
        {
            try
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Comment> GetLastComment()
        {
            try
            {
                return await _context.Comments.OrderByDescending(u => u.CommentId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
