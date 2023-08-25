using AutoMapper;
using QuickServiceWebAPI.DTOs.Problem;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class ProblemProfile : Profile
    {
        public ProblemProfile()
        {
            CreateMap<Problem, ProblemDTO>().
                ForMember(dest => dest.AssignerEntity,
                opt => opt.MapFrom(src => src.Assigner)).
                 ForMember(dest => dest.AttachmentEntity,
                opt => opt.MapFrom(src => src.Attachment)).
                 ForMember(dest => dest.GroupEntity,
                opt => opt.MapFrom(src => src.Group)).
                  ForMember(dest => dest.RequesterEntity,
                opt => opt.MapFrom(src => src.Requester));
        }
    }
}
