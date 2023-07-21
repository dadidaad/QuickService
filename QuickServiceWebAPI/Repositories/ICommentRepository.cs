using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface ICommentRepository
    {
        public List<Comment> GetComments();
        public Task<Comment> GetCommentById(string commentId);
        public Task AddComment(Comment comment);
        public Task UpdateComment(Comment comment);
        public Task DeleteComment(Comment comment);
        public Task<Comment> GetLastComment();
    }
}
