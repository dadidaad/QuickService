using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;
using System.Data;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class RoleRepository : IRoleRepository
    {
        private readonly QuickServiceContext _context;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(QuickServiceContext context, ILogger<RoleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int CountUserHaveRole(string roleId)
        {
            try
            {
                return _context.Users.GroupBy(u => u.RoleId)
                    .Where(u => u.Key == roleId).Count();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task CreateRole(Role role)
        {
            try
            {
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role with ID: {RoleId}", role.RoleId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }

        }

        public async Task DeleteRole(Role role)
        {
            try
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role with ID: {RoleId}", role.RoleId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task DeleteRolePermissions(Role role)
        {
            try
            {
                await _context.Entry(role).Collection("Permissions").LoadAsync();

                List<Permission> permissions = _context.Permissions
                    .Where(p => role.Permissions.All(pr => pr.PermissionId == p.PermissionId)).ToList();
                foreach (var permission in permissions)
                {
                    role.Permissions.Remove(permission);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role with ID: {RoleId}", role.RoleId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task<Role> GetLastRole()
        {
            try
            {
                return await _context.Roles.OrderByDescending(r => r.RoleId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task<Role> GetRoleById(string roleId)
        {
            try
            {
                return await _context.Roles.Include(r => r.Users).Include(r => r.Permissions).AsNoTracking().FirstOrDefaultAsync(r => r.RoleId == roleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role with ID: {RoleId}", roleId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public List<Role> GetRoles()
        {
            try
            {
                return _context.Roles.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving roles");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public List<Role> GetRolesByType(RoleType roleType)
        {
            try
            {
                return _context.Roles.Where(r => r.RoleType == roleType).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving roles");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task UpdateRole(Role existingRole, Role updateRole)
        {
            try
            {
                _context.Entry(existingRole).CurrentValues.SetValues(updateRole);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role with ID: {RoleId}", updateRole.RoleId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task UpdateUserRole(Role existingRole)
        {
            try
            {
                foreach (var userModel in existingRole.Users)
                {
                    var exsitingUser = existingRole.Users
                        .Where(u => u.UserId == userModel.UserId && u.UserId != default(string))
                        .FirstOrDefault();
                    if (exsitingUser != null)
                    {
                        _context.Entry(exsitingUser).CurrentValues.SetValues(userModel);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while update user");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
