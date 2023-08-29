using QuickServiceWebAPI.DTOs.Comment;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Services
{
    public interface ICommentService
    {
        public List<CommentDTO> GetCommentByUser(string userId);
        public List<CommentDTO> GetCustomerCommentsByRequestTicket(string requestTicketId);
        public List<CommentDTO> GetCommentsByRequestTicket(string requestTicketId);

        public List<CommentDTO> GetCommentsByProblem(string problemId);
        public List<CommentDTO> GetCustomerCommentsByProblem(string problemId);
        public List<CommentDTO> GetCommentsByChange(string changeId);
        public List<CommentDTO> GetCustomerCommentsByChange(string changeId);
        public Task<CommentDTO> CreateComment(CreateCommentDTO createCommentDTO);
        public Task<CommentDTO> CreateCommentChange(CreateCommentChangeDTO createCommentChangeDTO);
        public Task<CommentDTO> CreateCommentProblem(CreateCommentProblemDTO createCommentProblemDTO);
        public Task UpdateComment(UpdateCommentDTO updateCommentDTO);
        public Task DeleteComment(string commentId);
        public Task<string> GetNextId();
    }
}
