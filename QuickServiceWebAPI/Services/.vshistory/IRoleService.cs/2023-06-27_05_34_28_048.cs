﻿using QuickServiceWebAPI.DTOs.Role;

namespace QuickServiceWebAPI.Services
{
    public interface IRoleService
    {
        public Task CreateRole(CreateDTO createDTO);
        public Task UpdateRole(Role)
    }
}
