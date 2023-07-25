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
                .ForMember(dest => dest.AttachmentEntity,
                opt => opt.MapFrom(src => src.Attachment))
                .ForMember(dest => dest.CommentByUserEntity,
                opt => opt.MapFrom(src => src.CommentByNavigation))
                .ForMember(dest => dest.RequestTicketEntity,
                opt => opt.MapFrom(src => src.RequestTicket));
            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<UpdateCommentDTO, Comment>();
        }
    }
}
