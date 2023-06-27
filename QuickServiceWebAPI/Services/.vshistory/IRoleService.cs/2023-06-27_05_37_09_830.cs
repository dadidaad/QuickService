using QuickServiceWebAPI.DTOs.Role;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Services
{
    public interface IRoleService
    {
        public Task CreateRole(CreateDTO createDTO);
        public Task UpdateRole(UpdateDTO updateDTO);
        public Task DeleteRole(string roleId);
        public List<Role> GetRoles();
        public Task<Role> GetRoleById(string roleId);

    }
}
