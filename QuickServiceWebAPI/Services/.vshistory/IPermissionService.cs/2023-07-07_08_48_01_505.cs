using QuickServiceWebAPI.DTOs.Permission;

namespace QuickServiceWebAPI.Services
{
    public interface IPermissionService
    {
        public Task AssignPermissionsToRole(AssignPermissionsDTO assignPermissionsDTO);
        public Task<HashSet<string>> GetPermissionsByRole(string roleId);
    }
}
