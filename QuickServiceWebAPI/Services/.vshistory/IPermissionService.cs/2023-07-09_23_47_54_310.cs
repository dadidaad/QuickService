using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Services
{
    public interface IPermissionService
    {
        public Task AssignPermissionsToRole(AssignPermissionsDTO assignPermissionsDTO);
        public Task<HashSet<string>> GetPermissionsByRole(string roleId);
        public Task<List<Permission>> GetPermissionsByRoleType(RoleType roleType);
    }
}
