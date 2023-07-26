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
                .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<User, AuthenticateResponseDTO>();
            CreateMap<RegisterDTO, User>();
            CreateMap<UpdateDTO, User>().IgnoreAllNonExisting();
        }
    }
}
