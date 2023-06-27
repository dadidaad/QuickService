using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IRoleRepository
    {

        public List<Role> GetRoles();
        public Task CreateRole(Role role);
        public Task UpdateRole(Role existingRole, Role updateRole);
        public Task DeleteRole(Role role);
        public Task<Role> GetRoleById(string roleId);
        public Task<Role> GetLastRole();
        public List<Role> GetRolesByType(RoleType roleType);
        public Task UpdateUserRole(Role existingRole);
        public Task DeleteRolePermissions(Role role);
    }
}
