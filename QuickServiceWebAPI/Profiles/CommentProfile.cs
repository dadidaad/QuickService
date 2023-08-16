using AutoMapper;
using QuickServiceWebAPI.DTOs.Comment;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.CommentByUserEntity,
                opt => opt.MapFrom(src => src.CommentByNavigation));
            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<UpdateCommentDTO, Comment>();
        }
    }
}
