using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IPermissionRepository
    {
        public List<Permission> GetPermissions(Permission permission);
        public Task<List<Permission>> GetPermissionsByRoles(string roleId);
        public Task CreatePermission();
        public Task<List<Permission>> GetPermissionsByModule(string module);
        public Task UpdatePermission(Permission existingPermission, Permission updatePermission);
    }
}
