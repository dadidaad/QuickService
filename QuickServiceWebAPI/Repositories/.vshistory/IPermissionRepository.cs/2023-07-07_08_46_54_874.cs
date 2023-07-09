using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IPermissionRepository
    {
        public List<Permission> GetPermissions();
        public Task<List<Permission>> GetPermissionsByRole(string roleId);
        public Task CreatePermission(Permission permission);
        public Task<List<Permission>> GetPermissionsByModule(string module);
        public Task UpdatePermission(Permission existingPermission, Permission updatePermission);
    }
}
