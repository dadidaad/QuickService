using AutoMapper;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class QueryProfile : Profile
    {
        public QueryProfile() 
        {
            CreateMap<Query, QueryDTO>()
                .ForMember(dest => dest.UserEntity,
                opt => opt.MapFrom(src => src.User));
        }
    }
}
