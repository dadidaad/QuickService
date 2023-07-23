﻿using Microsoft.EntityFrameworkCore;
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
                Comment comment = await _context.Comments.Include(a => a.Attachment).Include(u => u.CommentByNavigation)
                                 .Include(r => r.RequestTicket).FirstOrDefaultAsync(x => x.CommentId == commentId);
                return comment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<Comment> GetComments()
        {
            try
            {
                return _context.Comments.Include(a => a.Attachment).Include(u => u.CommentByNavigation)
                                        .Include(r => r.RequestTicket).ToList();
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