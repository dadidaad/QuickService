using QuickServiceWebAPI.DTOs.Comment;

namespace QuickServiceWebAPI.Services
{
    public interface ICommentService
    {
        public List<CommentDTO> GetCommentByUser(string userId);
        public List<CommentDTO> GetCustomerCommentsByRequestTicket(string requestTicketId);
        public List<CommentDTO> GetCommentsByRequestTicket(string requestTicketId);
        public Task<CommentDTO> CreateComment(CreateCommentDTO createCommentDTO);
        public Task UpdateComment(UpdateCommentDTO updateCommentDTO);
        public Task DeleteComment(string commentId);
        public Task<string> GetNextId();
    }
}
