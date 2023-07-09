using QuickServiceWebAPI.DTOs.Permission;

namespace QuickServiceWebAPI.Services
{
    public interface IPermissionService
    {
        public Task AssignPermissionsToRole(AssignPermissionsDTO assignPermissionsDTO);
        public Task<List<string>> GetPermissionsByRole(string roleId);
    }
}
