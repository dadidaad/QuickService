using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class PermissionRepository : IPermissionRepository
    {
        public Task CreatePermission(Permission permission)
        {
            throw new NotImplementedException();
        }


        public List<Permission> GetPermissions()
        {
            throw new NotImplementedException();
        }

        public List<Permission> GetPermissions(Permission permission)
        {
            throw new NotImplementedException();
        }

        public Task<List<Permission>> GetPermissionsByModule(string module)
        {
            throw new NotImplementedException();
        }

        public Task<List<Permission>> GetPermissionsByRoles(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePermission(Permission existingPermission, Permission updatePermission)
        {
            throw new NotImplementedException();
        }
    }
}
