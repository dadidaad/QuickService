using QuickServiceWebAPI.Models;
using System.Data;
using System.Security;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly QuickServiceContext _context;
        private readonly ILogger<RoleRepository> _logger;

        public PermissionRepository(QuickServiceContext context, ILogger<RoleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreatePermission(Permission permission)
        {
            try
            {
                _context.Permissions.Add(permission);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role with ID: {PermissionId}", permission.PermissionId);
                throw;
            }
        }

        public List<Permission> GetPermissions()
        {
            try
            {
                return _context.Permissions.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role with ID: {PermissionId}", permission.PermissionId);
                throw;
            }
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
