using AutoMapper;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using System.Reflection;
using QuickServiceWebAPI.Utilities;
namespace QuickServiceWebAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Role,
                opt => opt.MapFrom(src => src.Role));
            CreateMap<User, AuthenticateResponseDTO>();
            CreateMap<RegisterDTO, User>();
            CreateMap<UpdateDTO, User>().IgnoreAllNonExisting();
        }
    }
}
