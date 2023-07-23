using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;

namespace QuickServiceWebAPI.Repositories
{
    public interface IPermissionRepository
    {
        public Task<List<Permission>> GetPermissionsForRoleType(RoleType roleType);
        public Task<List<Permission>> GetPermissionsByRole(string roleId);
        public Task CreatePermission(Permission permission);
        public Task UpdatePermission(Permission existingPermission, Permission updatePermission);
        public Task<Permission> GetPermission(string permissionId);
    }
}
