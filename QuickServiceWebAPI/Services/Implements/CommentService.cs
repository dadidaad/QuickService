using AutoMapper;
using QuickServiceWebAPI.DTOs.Comment;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
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

        public List<CommentDTO> GetCommentByUser(string commentId)
        {
            var comments = _repository.GetCommentByUser(commentId);
            return comments.Select(comment => _mapper.Map<CommentDTO>(comment)).ToList();
        }

        public List<CommentDTO> GetCustomerCommentsByRequestTicket(string requestTicketId)
        {
            var comments = _repository.GetCustomerCommentsByRequestTicket(requestTicketId);
            return comments.Select(comment => _mapper.Map<CommentDTO>(comment)).ToList();
        }

        public List<CommentDTO> GetCommentsByRequestTicket(string requestTicketId)
        {
            var comments = _repository.GetCommentsByRequestTicket(requestTicketId);
            return comments.Select(comment => _mapper.Map<CommentDTO>(comment)).ToList();
        }

        public async Task CreateComment(CreateCommentDTO createCommentDTO)
        {

            var comment = _mapper.Map<Comment>(createCommentDTO);
            User user = await _userRepository.GetUserDetails(comment.CommentBy);
            if (user.Role.RoleType.ToEnum(RoleType.Agent) == RoleType.Agent)
            {
                comment.IsInternal = false;
            }
            else
            {
                comment.IsInternal = true;
            }
            comment.CommentId = await GetNextId();
            await _repository.AddComment(comment);
        }

        public async Task UpdateComment(string commentId, UpdateCommentDTO updateCommentDTO)
        {
            Comment comment = await _repository.GetCommentById(commentId);
            if (comment == null)
            {
                throw new AppException("BusinessHour not found");
            }
            comment = _mapper.Map(updateCommentDTO, comment);
            await _repository.UpdateComment(comment);
        }

        public async Task DeleteComment(string commentId)
        {

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
