﻿using AutoMapper;
using QuickServiceWebAPI.DTOs.Role;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<CreateDTO, Role>();
            CreateMap<UpdateDTO, Role>().IgnoreAllNonExisting();
        }
    }
}
