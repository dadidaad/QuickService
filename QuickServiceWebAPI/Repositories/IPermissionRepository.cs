using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IPermissionRepository
    {
        public List<Permission> GetPermissions();
        public List<Permission> GetPermissionsByRoles(string roleId);
        public Task CreatePermission(Permission permission);
        public Task<List<Permission>> GetPermissionsByModule(string module);
        public Task UpdatePermission(Permission existingPermission, Permission updatePermission);
        public Task<Permission> GetLastPermission();
    }
}
