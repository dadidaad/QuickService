using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;

namespace QuickServiceWebAPI.Services
{
    public interface IPermissionService
    {
        public Task<List<Permission>> GetPermissions();
        public Task AssignPermissionsToRole(AssignPermissionsDTO assignPermissionsDTO);
        public Task<List<Permission>> GetPermissionsByRole(string roleId);
        public Task UpdatePermissionsToRole(UpdatePermissionsDTO updatePermissionsDTO);
        public Task<PermissionForRoleResponseDTO> GetPermissionsForRoleWithDTO(string roleId);
    }
}
