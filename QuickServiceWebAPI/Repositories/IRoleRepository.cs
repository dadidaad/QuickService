﻿using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;

namespace QuickServiceWebAPI.Repositories
{
    public interface IRoleRepository
    {

        public List<Role> GetRoles();
        public Task CreateRole(Role role);
        public Task UpdateRole(Role updateRole);
        public Task DeleteRole(Role role);
        public Task<Role> GetRoleById(string roleId);
        public Task<Role> GetLastRole();
        public List<Role> GetRolesByType(RoleType roleType);
        public Task UpdateUserRole(Role existingRole);
        public Task DeleteRolePermissions(Role role);
        public int CountUserHaveRole(string roleId);
    }
}
