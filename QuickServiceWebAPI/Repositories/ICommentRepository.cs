using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface ICommentRepository
    {
        public Task<Comment> GetCommentById(string commentId);
        public List<Comment> GetCommentByUser(string userId);
        public List<Comment> GetCommentsByRequestTicket(string requestTicketId);
        public List<Comment> GetCustomerCommentsByRequestTicket(string requestTicketId);
        public List<Comment> GetCommentsByProblem(string problemId);
        public List<Comment> GetCustomerCommentsByProblem(string problemId);
        public List<Comment> GetCommentsByChange(string changeId);
        public List<Comment> GetCustomerCommentsByChange(string changeId);
        public Task AddComment(Comment comment);
        public Task UpdateComment(Comment comment);
        public Task DeleteComment(Comment comment);
        public Task<Comment> GetLastComment();
    }
}
