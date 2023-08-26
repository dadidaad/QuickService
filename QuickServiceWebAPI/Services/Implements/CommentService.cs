using AutoMapper;
using QuickServiceWebAPI.DTOs.Comment;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CommentService(ICommentRepository repository, IUserRepository userRepository, IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public List<CommentDTO> GetCommentByUser(string userId)
        {
            var comments = _repository.GetCommentByUser(userId);
            if (comments == null)
            {
                throw new AppException($"user with Id {userId} not found");
            }
            return comments.Select(comment => _mapper.Map<CommentDTO>(comment)).ToList();
        }

        public List<CommentDTO> GetCustomerCommentsByRequestTicket(string requestTicketId)
        {
            var comments = _repository.GetCustomerCommentsByRequestTicket(requestTicketId);
            if (comments == null)
            {
                throw new AppException($"requestTicket with Id {requestTicketId} not found");
            }
            return comments.Select(comment => _mapper.Map<CommentDTO>(comment)).ToList();
        }

        public List<CommentDTO> GetCommentsByRequestTicket(string requestTicketId)
        {
            var comments = _repository.GetCommentsByRequestTicket(requestTicketId);
            if (comments == null)
            {
                throw new AppException($"requestTicket with Id {requestTicketId} not found");
            }
            return comments.Select(comment => _mapper.Map<CommentDTO>(comment)).ToList();
        }

        public async Task<CommentDTO> CreateComment(CreateCommentDTO createCommentDTO)
        {

            var comment = _mapper.Map<Comment>(createCommentDTO);
            comment.CommentId = await GetNextId();
            comment.CommentTime = DateTime.Now;
            await _repository.AddComment(comment);

            var createdCommentDTO = await _repository.GetCommentById(comment.CommentId);
            return _mapper.Map<CommentDTO>(createdCommentDTO);
        }

        public async Task UpdateComment(UpdateCommentDTO updateCommentDTO)
        {
            Comment comment = await _repository.GetCommentById(updateCommentDTO.CommentId);
            if (comment == null)
            {
                throw new AppException("Comment not found");
            }
            comment = _mapper.Map(updateCommentDTO, comment);
            comment.LastModified = DateTime.Now;
            await _repository.UpdateComment(comment);
        }

        public async Task DeleteComment(string commentId)
        {
            Comment comment = await _repository.GetCommentById(commentId);
            if (comment == null)
            {
                throw new AppException("Comment not found");
            }
            await _repository.DeleteComment(comment);
        }
        public async Task<string> GetNextId()
        {
            Comment lastComment = await _repository.GetLastComment();
            int id = 0;
            if (lastComment == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastComment.CommentId) + 1;
            }
            string commentId = IDGenerator.GenerateCommentId(id);
            return commentId;
        }
    }
}
