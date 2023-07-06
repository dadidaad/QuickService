using Microsoft.EntityFrameworkCore;
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
                _logger.LogError(ex, "An error occurred while retrieving permissions");
                throw;
            }
        }

        public async Task<List<Permission>> GetPermissionsByModule(string module)
        {
            try
            {
                return await _context.Permissions.Where(p => p.PermissionModule == module).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving permissions");
                throw;
            }
        }

        public List<Permission> GetPermissionsByRoles(string roleId)
        {
            try
            {
                return _context.Roles.Include(r => r.Permissions)
                    .FirstOrDefault(r => r.RoleId == roleId).Permissions.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving permissions by role with id: {roleId}", roleId);
                throw;
            }
        }

        public async Task UpdatePermission(Permission existingPermission, Permission updatePermission)
        {
            try
            {
                _context.Entry(existingPermission).CurrentValues.SetValues(updatePermission);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving permission with ID: {PermissionID}", existingPermission.PermissionId);
                throw;
            }
        }
    }
}
