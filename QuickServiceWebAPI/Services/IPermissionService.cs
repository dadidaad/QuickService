using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Services
{
    public interface IPermissionService
    {
        public Task<List<Permission>> GetPermissionsByRole(string roleId);
        public Task UpdatePermissionsToRole(UpdatePermissionsDTO updatePermissionsDTO);
        public Task<PermissionForRoleResponseDTO> GetPermissionsForRoleWithDTO(string roleId);
    }
}
