using QuickServiceWebAPI.Models;

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

        public Task DeleteRole(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetRoleById(string roleId)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetRoles()
        {
            throw new NotImplementedException();
        }

        public Task UpdateRole(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
