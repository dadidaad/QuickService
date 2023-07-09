using QuickServiceWebAPI.DTOs.Permission;

namespace QuickServiceWebAPI.Services
{
    public interface IPermissionService
    {
        public Task AssignPermissionsToRole(AssignPermissionsDTO);
    }
}
