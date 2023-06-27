using AutoMapper;
using QuickServiceWebAPI.DTOs.Role;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<CreateDTO, Role>();
            CreateMap<UpdateDTO, Role>();
        }
    }
}
