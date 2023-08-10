using AutoMapper;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class QueryProfile : Profile
    {
        public QueryProfile() 
        {
            CreateMap<Query, GetQueryDTO>()
                .ForMember(dest => dest.UserEntity,
                opt => opt.MapFrom(src => src.User));
        }
    }
}
