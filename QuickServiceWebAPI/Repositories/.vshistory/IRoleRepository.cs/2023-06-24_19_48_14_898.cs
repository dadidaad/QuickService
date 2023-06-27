using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IRoleRepository
    {

        public List<Role> GetRoles();
        public Task CreateRole(Role role);
        public Task UpdateRole(Role role);
        public Task DeleteRole(string roleId);
        public Task<Role> GetRoleById(string roleId);
    }
}
