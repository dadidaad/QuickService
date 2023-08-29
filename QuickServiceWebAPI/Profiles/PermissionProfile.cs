using AutoMapper;
using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<PermissionDTO, Permission>().IgnoreAllNonExisting();
            CreateMap<Permission, PermissionDTO>().IgnoreAllNonExisting();
        }
    }
}
